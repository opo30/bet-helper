var dom = (document.getElementsByTagName) ? true : false;
var ie5 = (document.getElementsByTagName && document.all) ? true : false;

var COOKIE_DELIMITER = "; ";
var EQUALS_DELIMITER = "=";

var COOKIE_VALUE_DELIMITER = "||";
var VALUE_PROPERTY_DELIMITER = "::";

var BETEX_COOKIE_NAME = "Betex_";
var BETEX_SESSION_COOKIE_NAME = "betexPtkSess";

var PREFERENCE_SIZE = 18;
//this array associates the long with the short name
//this allows us to write/read bR instead of betexRegion
var MAPPING = new Object();
	//MAPPING["full name of property"] = ["short name of property", "default value of property"];
	MAPPING["UserPreferencesShowAvailableFunds"] = ["upSAF", "true"];

//JPE_TODO implement session cookie compression client side.  will also need to do this sesver side.
var SESSION = new Array(9);
	SESSION["betexBrand"] = "bB";
	SESSION["betexRegion"] = "bR";
	SESSION["betexLocale"] = "bL";
	SESSION["betexCMSLocale"] = "bCMSL";
	SESSION["betexTimeZone"] = "bTZ";
	SESSION["betexCurrency"] = "bC";
	SESSION["betexRegionSessionCookie"] = "bRSC";
	SESSION["betexLocaleSessionCookie"] = "bLSC";
	SESSION["betexCMSLocaleSessionCookie"] = "bCMSLSC";
	SESSION["betexTimeZoneSessionCookie"] = "bTZSC";
	SESSION["betexCurrencySessionCookie"] = "bCSC";


//JPE_TODO implement this.  small gain false * 18 = 90 characters, f * 18 = 18.  72 characters saved
var VALUE_MAPPING = new Array(5);
	VALUE_MAPPING["true"] = "t";
	VALUE_MAPPING["false"] = "f";
	VALUE_MAPPING["Green"] = "g";
	VALUE_MAPPING["Yellow"] = "y";
	VALUE_MAPPING["Red"] = "r";
	
	
function DoRnd(iNum,iDP){
	if(iNum == 0) {
		return 0;
	}
	return Math.round(iNum*Math.pow(10,iDP))/Math.pow(10,iDP);
}

function isInteger(passedValue) {
    return !/\D/.test(passedValue);
}

function isFloat(passedValue){
    return /^\d+(\.\d+)?$/.test(passedValue);
}

function toFixedDP(iNum, iDP){	
	
	var checkNaN = Number(iNum);	
	if(isNaN(checkNaN)){
		return iNum;
	}
		
    var roundNum, stringNum, padding = '';
	
	for(var i=0;i<iDP;i++){
		padding += '0';
	}
	
	if(!isFloat(iNum)){
		iNum = checkNaN;				
	}
		
    stringNum = DoRnd(iNum, iDP) + '';
    if (stringNum.indexOf('.') != -1) {
        stringNum = stringNum + padding;
    }
    else {
        stringNum = stringNum + '.' + padding;
    }
    
	roundNum = stringNum.substring(0, stringNum.indexOf('.') + iDP + 1);
	
    return roundNum;
}


function formatCurrency(sCurrency, iAmount, iDP){ 	
	if(!isInteger(iDP)){
		iDP = 2;
	}	
	var amount = toFixedDP(Math.abs(iAmount), iDP)+''; 	
    var parts = amount.split('.');
    var formatted = '';
    for (var i=parts[0].length;i>=0;i-=3) {
        formatted = parts[0].substring(i-3, i) + formatted;
        if (i>3) {
            formatted = ',' + formatted;
        }
    }	
    return (iAmount<0 ? '-' : '') + sCurrency + formatted + '.' + parts[1];
}

function formatWholeNumber(iAmount, iDP){
	var newAmount;
	if(iAmount == Math.round(iAmount)){
		return iAmount;
	}else{
		newAmount = (Number(iAmount)==Math.floor(Number(iAmount)))?Math.abs(iAmount)+'.00':((Number(iAmount)*10==Math.floor(Number(iAmount)*10))?Math.abs(iAmount)+'0':Math.abs(iAmount));
		return newAmount;
	}
}

/*
*	Get the named cookie
*	[0] is the cookie's name after the first split
*	[1] is the cookie's value after the second split
*/
function getUserPreferenceCookie(cookieName) {
	if(document.cookie != "") {
		var allCookies = document.cookie.split(COOKIE_DELIMITER);

		for(i = 0; i < allCookies.length; i++) {
			if(allCookies[i].split(EQUALS_DELIMITER)[0] == cookieName) {
				return allCookies[i].split(EQUALS_DELIMITER)[1];
			}
		}
	}
	return null;
}
	
/*
*
*/
function getUserPreference(sAccountID) {
	var valuesArray = getUserPreferenceCookie(BETEX_COOKIE_NAME + sAccountID);

	if(valuesArray == null) {
		return null;
	}

	valuesArray = unescape(valuesArray).split(COOKIE_VALUE_DELIMITER);

	var nameValueArray = new Object();
	for(i = 0; i < valuesArray.length; i++)	{
		var tmpArray = valuesArray[i].split(VALUE_PROPERTY_DELIMITER);
		nameValueArray[tmpArray[0]] = tmpArray[1];
	}
	return nameValueArray;
}

/*
*	find a single value based on a key and account
*/
function findUserPreference(sAccountID, preferenceName) {

	var preferenceArray = getUserPreference(sAccountID);
	if(preferenceArray == null) {
		preferenceArray = constructDefaultUserPreference();
	}

	return preferenceArray[MAPPING[preferenceName][0]];	
}

/*
*	construct an array of short names and default values
*	this is only done once, unless the cookie is dropped or deleted
*/
function constructDefaultUserPreference() {

	var preferenceArray = new Object();

	for(map in MAPPING) {	
		preferenceArray[MAPPING[map][0]] = MAPPING[map][1];
	}
	return preferenceArray;
}

/*
*	Is this preference key one of the User Preferences
*/
function isUserPreference(key) {
	if(MAPPING[key] && MAPPING[key][0] != "") {	
		return true;
	}
	return false;
}

/*
*	Utility function for getting the mapping
*/
function getUserPreferenceMapping(key) {
	if(isUserPreference(key)) {
		return MAPPING[key][0];
	}
}

/*
*	Utility function for getting the mapping default value
*/
function getUserPreferenceDefault(key) {
	if(isUserPreference(key)) {
		return MAPPING[key][1];
	}
}

/*
*	Utility function for getting the mapping
*/
function getUserSessionMapping(key) {
	if(isSessionCookie(key)) {
		return SESSION[key];
	}
}

/*
*	construct the entire cookie value and then set this cookie "Betex_accountID=   abc::123||def::456"	
*	
*	before constructing the new cookie value, check to see the the preference value has changed
*	if it hasn't then don't do anything as nothing has changed
*/
function constructUserPreference(sAccountID, preferenceName, preferenceValue) {

	var preferenceArray = getUserPreference(sAccountID);
	
	
	if(preferenceArray == null) {
		preferenceArray = constructDefaultUserPreference();
	}

	var userPreference = "";

	var i = 0;
	for(preference in preferenceArray) {
		//is this the one that needs updating?
		if(MAPPING[preferenceName][0] == preference) {
			//this name/value pair already exists in the array and is the one being updated, so just write to the string
			userPreference = userPreference + preference + VALUE_PROPERTY_DELIMITER + preferenceValue;
		}
		else {
			//this name/value pair already exists in the array and is not the one being updated, so just write to the string
			//shortname::value
			userPreference = userPreference + preference + VALUE_PROPERTY_DELIMITER + preferenceArray[preference];
		}

		i++;
		if(i < preferenceArray.length) {
			//add the ||
			userPreference = userPreference + COOKIE_VALUE_DELIMITER;
		}
	}	
	return BETEX_COOKIE_NAME + sAccountID + "=" + escape(userPreference);
}

function isSessionCookie(key) {
	if(SESSION[key] && SESSION[key][0] != "") {	
		return true;
	}
	return false;
}

function getSessionCookieValue(sessionCookieName, sessionCookieValue) {

	var sessionArray = (unescape(sessionCookieValue)).split("~");
	
	for(var i = 0; i < sessionArray.length; i++) {
		var temp = sessionArray[i].split("=");
		if(temp[0] == sessionCookieName) {
			return temp[1];
		}

	}
}

function getSessionCookie(sessionCookieName) {
	var search = BETEX_SESSION_COOKIE_NAME + "=";
	var returnValue = "";
	if(document.cookie.length > 0) {
		offset = document.cookie.indexOf(search)
		// if cookie exists
		if (offset != -1) { 
			offset += search.length;
			// set index of beginning of value
			end = document.cookie.indexOf("; ", offset);
			// set index of end of cookie value
			if(end == -1) {
				end = document.cookie.length;
			}
			returnValue = getSessionCookieValue(sessionCookieName, unescape(document.cookie.substring(offset, end)));
		}
	}
	return returnValue;
}

function GetCookie(sCookieName, sAccountID) {
	//check to see if this puppy is a session value
	if(isSessionCookie(sCookieName)) {
		return getSessionCookie(sCookieName);
	}
 
	//check to see if this name is a user preference cookie value
	if(isUserPreference(sCookieName)) {
		return findUserPreference(sAccountID, sCookieName);
	}
	
	var sSearch = sCookieName + ((sAccountID != "" && sAccountID != null) ? "_" + sAccountID : "") + "=";
	var sCookie = "";
	var iOffset = -1;
	var bBreakOut = false;
	var sTempCookie = document.cookie
	if(sTempCookie.length > 0) {
		// this is to ensure this is the name cookie and not one that contains a substring of it's name
		// e.g. userhistory & xguserhistory
		// or userhistory & userhistoryxg
		while (!bBreakOut) {
			iOffset = sTempCookie.indexOf(sSearch);
			if (iOffset > 1) {
				if (sTempCookie.substring(iOffset-2, iOffset) == "; ") {
					bBreakOut = true;		
				} else {
					sTempCookie = sTempCookie.substring(iOffset + sSearch.length, sTempCookie.length);
				}
			} else if (iOffset == 0 || iOffset == -1) {
				bBreakOut = true;
			}
		}
		if(iOffset >= 0) {
			iOffset += sSearch.length;
			var iEnd = sTempCookie.indexOf(";", iOffset);
			if(iEnd == -1) { 
				iEnd=sTempCookie.length; 
			}
			sCookie = unescape(sTempCookie.substring(iOffset, iEnd));
		}
	}
	return sCookie;
} 

function SetCookie(sAccountID, sCookieName, sCookieValue, sNoEscape) {
	var sSearch;

	//check to see if this name is a user preference cookie value	
	if(isUserPreference(sCookieName)) {
		sSearch = constructUserPreference(sAccountID, sCookieName, sCookieValue);
	}
	else {
		sSearch = sCookieName+(sAccountID!=""?"_"+sAccountID:"")+"=";	
	}
	
	var JSessionIdValue=GetCookie("JSESSIONID");
	var BSessionIdValue=GetCookie("bsessionid");
	var cookieSavepoint=checkpointCookies();
	
	var newCookieStr = "";
	var oldCookie = null;

	var sExpires = new Date();
	sExpires.setTime(sExpires.getTime() + 365*24*60*60*1000);
	var expiresStr = " ; path=/ ; expires=" + sExpires.toGMTString();

	if(isUserPreference(sCookieName)) {
		newCookieStr = sSearch + expiresStr;

	}
	else {
		if (sNoEscape) {
			newCookieStr = sSearch + sCookieValue + expiresStr;
		} else {
			newCookieStr = sSearch + escape(sCookieValue) + expiresStr;
		}
	}
			
	//Append '.' in front of domain, to share UIView Cookie with sub-domains
	if(sCookieName == constants_UI_VIEW_COOKIE 
	|| sCookieName == constants_BSP_LP_COOKIE
	|| sCookieName == constants_BSP_MD_COOKIE
	|| sCookieName == constants_USER_HISTORY_COOKIE 
	|| sCookieName == constants_SHOW_HELP_COOKIE
	|| sCookieName == constants_LAY_COLOUR_COOKIE
	|| sCookieName == constants_AUTO_REFRESH_COOKIE
	|| sCookieName == constants_SHOW_DEMO_HELP
	|| sCookieName == constants_OPEN_SUB_MENU
	|| sCookieName == constants_MINI_GAMES_ENABLED_YN
	|| sCookieName == constants_DISPLAY_MINI_GAMES_YN
	|| sCookieName == constants_MINI_GAMES_MIN_MAX
	|| isUserPreference(sCookieName)) {
		newCookieStr = newCookieStr + "; domain=." + document.domain;	
	}	
	document.cookie = newCookieStr;
    
	checkpointCookies(cookieSavepoint);
	
		//--2.5 IE drops cookies when 20 cookies exceeded. If that cookie is JSESSIONID then the user is logged out :-(
		//--method #defendCookie is a workaround for this.
	defendCookie("JSESSIONID",JSessionIdValue);
	defendCookie("bsessionid",BSessionIdValue);
}

function defendCookie(sCookieName,sCookieValue) {
	if (sCookieValue!=GetCookie(sCookieName)) {
		if(allowDebugAlerts){
			alert("Utilities.js#defendCookie("+sCookieName+","+sCookieValue+")-!!WARNING:BROWSER ANOMALY DETECTED!!\n\t"+sCookieName+" dropped. Attempting to re-instate it... ");
		}
		document.cookie=sCookieName+"="+escape(sCookieValue)+" ; path=/";
	}
}

function checkpointCookies(sCookieSavePoint) {
	if (allowDebugAlerts) {
		var sCookieCheckpoint=document.cookie+"";
		if (sCookieSavePoint!=undefined&&sCookieSavePoint!=null) {
			//-- check for inconsistencies: specifically if any cookies have disappeared.
			//   remember that cookie can expire naturally so a missing cookie is sometimes the correct beahviour
			var cookieCheckpointArray=sCookieCheckpoint.split(";");
			var cookieSavepointArray=sCookieSavePoint.split(";");
			checkpointCookieDiff(cookieSavepointArray,cookieCheckpointArray);
		} 
		else {
			//-- return cookie savepoint
			return sCookieCheckpoint;
		}
	} 
}

//-- WARNING: method #checkpointCookieDiff contains debug alerts.
//            it should only be called by #checkpointCookies which is protected by 'allowDebugAlerts'
function checkpointCookieDiff(savepointCookieArray,checkpointCookieArray) {
	if ( (savepointCookieArray==undefined||savepointCookieArray==null||savepointCookieArray.length==0) ||
	     (checkpointCookieArray==undefined||checkpointCookieArray==null||checkpointCookieArray.length==0) )
		return;

	var diffArray=new Array();
	for (var idxSavepoint=0;idxSavepoint<savepointCookieArray.length;idxSavepoint++) {
		var found=false;
		var savepointCookieName=(savepointCookieArray[idxSavepoint].split("="))[0];
		for (var idxCheckpoint=0;idxCheckpoint<checkpointCookieArray.length;idxCheckpoint++) {
			//alert("Utilities.js#checkpointCookieDiff()\n\nChecking for '"+savepointCookieName+"' as substring of '"+checkpointCookieArray[idxCheckpoint]+"'");
			if (checkpointCookieArray[idxCheckpoint].indexOf(savepointCookieName)>=0) {
				//alert("Utilities.js#checkpointCookieDiff()\n\nFound '"+savepointCookieName+"' as substring of '"+checkpointCookieArray[idxCheckpoint]+"'");
				found=true;
				break;
			}
		}
		if (!found) {
			diffArray[diffArray.length]=savepointCookieArray[idxSavepoint];
		}
	}
	if (diffArray.length>0) {
	  var sDisplay="";
		sDisplay = "Utilities.js#checkpointCookies()\n\n!!WARNING!! Cookies dropped by browser!!\n";
		for (var idx = 0; idx < diffArray.length; idx++) {
		  sDisplay+=("["+idx+"]=>'"+diffArray[idx]+"'\n");
		}
		alert(sDisplay);
	}
}

function interface_getHelpKey(key, sAnchor) {
	spawn('help', key, sAnchor);
}

var sLastOpenStr;
function displayMoreInfo(eventId, selectionId, asianLineId, exchangeID, sOrigin) {
	var baseURL = getExchangeURL(exchangeID);
	var sOpenStr = "?marketId=" + eventId;
	sOpenStr = sOpenStr + "&selectionId=" + selectionId;
	if (asianLineId != null) {
		sOpenStr = sOpenStr + "&asianLineId=" + asianLineId;
	}
	if (sOrigin) {
		sOpenStr = sOpenStr + "&origin=" + sOrigin;
	}
	spawn('moreInfoChart', sOpenStr, baseURL);
	sLastOpenStr = sOpenStr;
}

function getExchangeURL(iExchangeID) {
	return betex_getProductDomain(iExchangeID);
}

function getOddsEquivSpawn(marketId, selectionId, price, size, bidType, betType, exchangeID){
	var baseURL = getExchangeURL(exchangeID);
	var sOpenStr = baseURL + ((betType == "Range")? "/betting/RangeOddsEquiv.do?" : "/betting/LineOddsEquiv.do?");
		sOpenStr += "marketId="+marketId+
		"&selectionId="+selectionId+
		"&price="+((price == null)? 0 : price)+
		"&sizeAmount="+((size == null)? 0 : size)+
		"&bidType="+bidType;
	spawn('oddsEquivalent',sOpenStr);
}

try{
	interface_registerJSResource("utilities")
}catch(x){
	
}

function isMozilla() {
	return ((navigator.userAgent.toLowerCase().indexOf("mozilla") != -1) && (navigator.appName.toLowerCase().indexOf("microsoft") == -1) && (navigator.userAgent.toLowerCase().indexOf("safari") == -1));
}

function isSafari() {
	return ((navigator.userAgent.toLowerCase().indexOf("safari") != -1) && (navigator.appName.toLowerCase().indexOf("microsoft") == -1));
}

function AjaxObject(parentController) {
	this.parentController = parentController;
	this.xmlHTTP = null;
	this.timeoutId = null;
	try {
		this.xmlHTTP = new XMLHttpRequest();
    } catch(e) {
		try {
			this.xmlHTTP = new ActiveXObject("Msxml2.XMLHTTP");
		} catch(e) {
			var success = false;
			var MSXML_XMLHTTP = ["MSXML2.XMLHTTP.5.0","MSXML2.XMLHTTP.4.0","MSXML2.XMLHTTP.3.0","MSXML2.XMLHTTP","Microsoft.XMLHTTP"];
	    	for(var i=0; i<MSXML_XMLHTTP.length && !success; i++) {
	        	try {
	            	this.xmlHTTP = new ActiveXObject(MSXML_XMLHTTP[i]);
	            	success = true;	           
				} catch(e) {
				    this.xmlHTTP = null;
				}
			}
	    }
	}
}
;AjaxObject.prototype.busy = function() {
    switch(this.xmlHTTP.readyState) {
        case 1,2,3:
            return true;
        	break;	        
        default: // 4,0
			return false;
        	break;
    }
}
;AjaxObject.prototype.sendRequest = function(requestType,requestURL,requestData) {
	var self = this;
	this.timeoutId = window.setTimeout(function(){ 
		if(self.busy()){
			self.xmlHTTP.abort();
		} }, 100);

	this.xmlHTTP.open(requestType,requestURL,true);
	try {
		this.xmlHTTP.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
		this.xmlHTTP.overrideMimeType("text/xml");
	} catch(e) {}
    this.xmlHTTP.onreadystatechange = function() {
    	self.callback();
    }
	this.xmlHTTP.send(requestData);
}
;AjaxObject.prototype.get = function(requestURL) {
	if(!this.timeoutId) {
		this.sendRequest("GET",requestURL,null);
	}
}
;AjaxObject.prototype.getJSONObject = function(requestURL, _jsonHandler) {
	if(!this.timeoutId) {
		this.json = true;
		//alert(callback);
		this.jsonHandler = _jsonHandler;
		this.sendRequest("GET",requestURL,null);
	}
}
;AjaxObject.prototype.post = function(requestURL,requestData) {
	if(!this.timeoutId) {
		if(typeof requestData == "object") {
			requestData = this.toString(requestData);
		}
//		alert(requestURL + "\n" + requestData);
		this.sendRequest("POST",requestURL,requestData);
	}
}
;AjaxObject.prototype.toString = function(data) {
	var s = "";
	for(var name in data) {
		if(typeof data[name] != "undefined" && typeof data[name] != "function") {
			s += "&" + escape(name) + "=" + escape(data[name]) ;
		}
	}
	return (s.substring(1));
}
;AjaxObject.prototype.callback = function() {
	switch(this.xmlHTTP.readyState) {
		case 4:
			window.clearTimeout(this.timeoutId);
			try {
				switch(this.xmlHTTP.status) {
					case 200:
						try {				
							if (this.json==true) {	
								jsonObj = eval( '(' + this.xmlHTTP.responseText + ')' );								
								this.jsonHandler(jsonObj);	
								//this.parentController.receiveWallet(jsonObj);							#
								this.timeoutId = null;
							} else {
								this.handleResponse(this.xmlHTTP.responseXML);
							}
						} catch(e) {
							this.handleResponse(null);
						}
						break;
					case 404:
					default:
						this.handleResponse(null);
						//alert(this.xmlHTTP.status + " : " + this.xmlHTTP.statusText);
						break;
				}
			} catch(e) {
				this.handleResponse(null);
			}
			break;
		case 3,2,1,0:
		default:
			break;
	}
}
;AjaxObject.prototype.handleResponse = function(XMLDocument) {
	
	if(XMLDocument && XMLDocument.nodeType == 9) {
		if(this.parentController) {
			this.parentController.handleAjaxResponse(XMLDocument);
		} else {
//			eval(this.normalize(XMLDocument.documentElement));
		}
	} else {
//		error handling goes here
//		alert("Error message goes here...");
	}
	this.timeoutId = null;
}
;AjaxObject.prototype.normalize = function(XMLNode){
	var normalized = "";
	if(XMLNode.hasChildNodes()) {
		var nodes = XMLNode.childNodes;
		var i = 0;
		do {
			if(nodes[i].nodeType == 3) {
				normalized += nodes[i].nodeValue;
			}
		} while(++i < nodes.length);			
	}
	return(normalized);
};

function IFrameAjaxRequest () {

	//AJAX status
	//0 = uninitialized
	//1 = setup not sent
	//2 = request in progress
	//3 = response in progress (not used in this implementation)
	//4 = request complete
	
	var self = this;
    var status = 0;
    var iframe = null;
    var id = 'IFrameAjaxRequest.' + Math.random();
    var ajaxTimeoutId = null;
    
    var _createIFrame = function (url) {
    	if (iframe == null) {
    		iframe = document.createElement('iframe');
			if (url.indexOf("https://") == 0) {
				if (top && top.frames['header'])
				iframe.src = top.frames['header'].userWalletSecureIFrameSource;
			} else {
				iframe.src = "";
			}
		    iframe.id = id;		    
		    if (iframe.addEventListener) {
		    	iframe.addEventListener('load', window[id].handleResponse, false);
		    } else {
		    	iframe.attachEvent('onload', window[id].handleResponse);
		    }
		    iframe.style.display =  'none';		    
		    document.body.appendChild(iframe);
		    document.close();
    	}
    }
    
    var _iframeHTML = function () {
    	try {
	    	return _iframeDocument().body.innerHTML;
    	}
    	catch (x) {
    		return null;
    	}
    }
    
    var _iframeDocument = function () {
    	try {
	    	var doc = null;
			if (iframe.contentDocument) { // For NS6
		    	doc = iframe.contentDocument; 
		  	} else if (iframe.contentWindow) { // For IE5.5 and IE6
		    	doc = iframe.contentWindow.document;
		  	} else if (iframe.document) { // For IE5
		    	doc = iframe.document;
		  	} else { // damn!
		    	alert("Error: could not find IFRAME document");
		  	}
		  	//doc.write(' ');
		  	//doc.close();
	  		return doc;
    	}
    	catch (x) {
    		return null;
    	}
	}
	
	var _register = function () {
		//global callback
		window[id] = self;		
	}
	
	var _unregister = function () {
		//unregister global callback
		window[id] = undefined;
		
	}
	
	var _serializeData = function(data) {
    	var s = "";
    	for(var name in data) {
    		if(typeof data[name] != "undefined" && typeof data[name] != "function") {
    			s += "&" + escape(name) + "=" + escape(data[name]) ;
    		}
    	}
    	return (s.substring(1));
    }
    
	
	this.get = function (url) {
		//only service request if not in progress
		if (status==0) { 
			status = 1;
			_register();
	    	_createIFrame(url);	              
			if (url.indexOf("?origin=initiate") == -1) {
		        ajaxTimeoutId = window.setTimeout(
		        	function () {
		        		self.handleTimeout();
		        		ajaxTimeoutId = null;
		        		_unregister();	  
		        		status = 0;
		        	}, 30000);
		        iframe.src = url;	  
		        status = 2;
			}
        }
    }
    
    this.post = function(requestURL,requestData) {
    	if(!this.timeoutId) {
    		if(typeof requestData == "object") {
    			requestData = _serializeData(requestData);
    		}
    		this.get(requestURL + "?" + requestData);
    	}
    }
    
    this.handleResponse = function () {	 
    	if (ajaxTimeoutId!=null) {
	   		status = 4;    	
	   		window.clearTimeout(ajaxTimeoutId);
		    ajaxTimeoutId = null;

		    try {
		        self.handleResponseHTML (self, _iframeHTML());
		    } catch (e) {
		        //invoke the error handler
		        try {
		           self.handleError();
		        } catch (e1) {
		            //squash
		        }
		    }

	    	_unregister();	  
	    	status = 0;
	    }
	    return true;
    }

    
    

};
IFrameAjaxRequest.prototype.handleResponseHTML = function (request, html) {
	//alert("Received: "+html);
};
IFrameAjaxRequest.prototype.handleTimeout = function () {
	//alert("AJAX Timeout");
};
IFrameAjaxRequest.prototype.handleError = function () {
    //alert("AJAX Error");
	this.handleTimeout();
};
function JSONRequest (callbackHandler, context, timeoutHandler) {
	IFrameAjaxRequest.call(this);
	this.callbackHandler = callbackHandler;
	if (context) {
		this.context = context;
	} 
	if (timeoutHandler) {
		this.timeoutHandler = timeoutHandler;
	}
};
JSONRequest.prototype = new IFrameAjaxRequest();

function stripScripts(htmlStr) {
    var scriptFragment = '(?:<script.*?>)((\n|\r|.)*?)(?:<\/script>)';
    return htmlStr.replace(new RegExp(scriptFragment, 'img'), '');
};

JSONRequest.prototype.handleResponseHTML = function (request, html) {
	var obj = null;
	var htmlStr = null;
	if (html == null) {
		var cookieIndex = document.cookie.indexOf("WPID" + request.context);
		if (cookieIndex != -1) {
			var startIndex = (document.cookie.indexOf("=", cookieIndex) + 1);
			var endIndex = (document.cookie.indexOf(";", startIndex) == -1 ) ? document.cookie.length : document.cookie.indexOf(";", startIndex) ;
			var walletBase64 = document.cookie.substring(startIndex, endIndex);
			html = decode64(walletBase64);
			var exdate = new Date();
			exdate.setSeconds(exdate.getSeconds()-1);
			document.cookie = "WPID" + request.context + "=;expires=" + exdate.toGMTString()  + ";expires=" + exdate.toGMTString() + ";path=/;domain=" + document.domain;
		}
	}
	if(html != null){
		htmlStr = html.toString();
		htmlStr = htmlStr.replace(/^\s+|\s+$/, '');
		htmlStr = stripScripts(htmlStr);
		if(htmlStr.length >0){
			try {
				obj = eval( '(' + htmlStr + ')' );
			}
			catch (e) {
			}
		}
	}
	if (request.context) {
		request.callbackHandler(obj, request.context);
	} else {
		request.callbackHandler(obj);
	}

};
JSONRequest.prototype.handleTimeout = function () {
    //alert("AJAX Timeout");
	if (this.timeoutHandler) {
		this.timeoutHandler(this.context);
	}
};

var Utilities = {};

// ** Start of new namespaces utilities functions.
Utilities.serializeData = function(data) {
	var s = "";
	for(var name in data) {
		if(typeof data[name] != "undefined" && typeof data[name] != "function") {
			s += "&" + escape(name) + "=" + escape(data[name]) ;
		}
	}
	return (s.substring(1));
}

var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
function decode64(input) {
	var output = "";
	var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
	var i = 0;
	var base64test = /[^A-Za-z0-9\+\/\=]/g;
	input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");
	do {
		enc1 = keyStr.indexOf(input.charAt(i++));
		enc2 = keyStr.indexOf(input.charAt(i++));
		enc3 = keyStr.indexOf(input.charAt(i++));
		enc4 = keyStr.indexOf(input.charAt(i++));
		chr1 = (enc1 << 2) | (enc2 >> 4);
		chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
		chr3 = ((enc3 & 3) << 6) | enc4;
		output = output + String.fromCharCode(chr1);
		if (enc3 != 64) {
			output = output + String.fromCharCode(chr2);
		}
		if (enc4 != 64) {
			output = output + String.fromCharCode(chr3);
		}
		chr1 = chr2 = chr3 = "";
		enc1 = enc2 = enc3 = enc4 = "";
	} while (i < input.length);
	return output;
}
