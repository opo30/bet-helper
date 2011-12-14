// ********** CONSTANTS FOR ARRAY DIMENSION ACCESS **********
var iCurrentMarketID = null;
var iCurrentExchangeID = null;
var NODE_TYPE = 0;	// 1 = market, 0 = non market
var ID = 1;
var PARENT_ID = 2;
var NAME = 3;
var HAS_MARKET = 4;	// 0 = has no markets, 1 = has markets, 2 = is market

// Node type: market (1)
var MARKET_TYPE = 5;
var BET_DELAY = 6;
var INTERFACE_ID = 7;
var START_TIME = 8;
var REGION_ID = 9;
var EXCHANGE_ID = 10;
var M_ALIAS = 11;
var M_DISPLAY_STYLE = 12;
var M_HOME_PATH = 13;

// Node type: non-market (0)
var COMPETITION_ID = 5;
var ALIAS = 6;
var DISPLAY_STYLE = 7;
var HOME_PATH = 8;

var MP_COMPETITION_ID = 4; // Menu path array 

var COMPETITION_ID_NULL = -2147483648


var oRegex = new RegExp("locale.*&brand", "gi");
var sLocale = location.href.match(oRegex);
if (sLocale == null) {
	var oRegex = new RegExp("locale.*", "gi");
	sLocale = location.href.match(oRegex) + "&brand";
}
sLocale = "" + sLocale;
sLocale = "&" + sLocale.substring(0,sLocale.length-6);

oRegex = new RegExp("region.*&locale", "gi");
var sRegion = location.href.match(oRegex);
if (sRegion == null) {
	var oRegex = new RegExp("region.*", "gi");
	sRegion = location.href.match(oRegex) + "&locale";
}
sRegion = "" + sRegion 
sRegion = "&" + sRegion .substring(0,sRegion .length-7);


// ********** ARRAY FOR THE MOVE ALL FUNCTION (FOR CUSTOMISE MENU) **********
var oAddAllArray = new Array();

// ********** ARRAYS CONTAINING THE EVENTS AND MARKETS INFORMATION **********
var popularSkeletonArray = new Array();
var popularSkeletonMap = new Array();
var allSkeletonArray = new Array();
var allSkeletonMap = new Array();
var mySkeletonArray = new Array();
var myMarketsArray = new Array();
var meatArray = new Array();
var meatMap = new Array();
var emptySkeletonArray = new Array();

// ********** ARRAYS CONTAINING THE PATH ITEMS **********
var popularPathArray = new Array();
var allPathArray = new Array();
var myPathArray = new Array();

// ********** ARRAY CONTAINING THE HTML THAT IS PAINTED TO THE SCREEN **********
var aHTML = new Array();

// ********** ARRAY CONTAINING THE OPENING AND CLOSING TAGS FOR EACH CONTAINER **********
var sContainerArray = new Array(2);
sContainerArray[1] = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\n";
sContainerArray[2] = "</table>\n";

// ********** COUNTERS THAT INDICATE THE ID OF THE CURRENT SELECTED ITEM FOR EACH TYPE OF MARKET **********
var currentlyShownMarketsId = 0; // used to avoid the FF double click feature
var currentlyShownMarketIdTimeout = null;

var currentPopItem;
var currentAllItem;
var currentMyItem;
var m_meatArray = "";
var iRequestFromOutsideParentID = null;
var iCouponParentID;
var lastMeatLoadedParentId = 0;
var oPathArray = new Array();
var oIDArray = new Array();

var iFlushMeatCacheTimer; // how long in seconds between flushing of the meat cache
//var oFlushMeatTimeout = setTimeout("flushMeatCache()", iFlushMeatCacheTimer*1000);

var isInMyMarketInvalidatorRequest = new Object();
var marketFrameworkRequest = new Object();
if (typeof(JSONRequest) != "undefined") {
	var cachePrimer = new JSONRequest(handleCachePrimerResponse, 1, handleCachePrimerTimeout);
}
var marketFrameworkURL = null;
try {
	marketFrameworkURL = "http://" + top.dMgr.getApplication("sports").constructContentUrl("betting/api/json/retrieveMarketFramework.do?");
	marketFrameworkRequest = new JSONRequest(handleMarketFrameworkResponse, 1, handleMarketFrameworkTimeout);
	isInMyMarketInvalidatorRequest = new JSONRequest(null, 1, null);
} catch (e) {
// my markets being called
}

var MENUD_ID_SAME = "";
var MENUD_ID_BACK = "-";
var MENUD_ID_ALLSPORTS = "allSportsTreeContainerParent";
var MENUD_ID_POPULARSPORTS = "popularSportsTreeContainerParent";
var MENUD_ID_MYMARKETS = "myMarketsTreeContainerParent";
var MENUD_ID_MULTIPLES = "multiplesTreeContainerParent";
var MENUD_ID_LASTSEARCH = "lastSearchTreeContainerParent";
var MENU_ORIGIN_DEFAULT = "LHM";
var MENU_ORIGIN_ALLSPORTS = "LHMA";
var MENU_ORIGIN_POPULARSPORTS = "LHMP";
var MENU_ORIGIN_MYMARKETS = "LHMM";
var MENU_ORIGIN_MULTIPLES = "LHMX";
var MENU_ORIGIN_LASTSEARCH = "LHMS";
var currentMenuId = "";
var prevMenuId = "";
var currentMenuTitle = "";
var prevMenuTitle = "";
var isIntegrateMultiples = false;
var allowDebugAlerts = false;
var marketRequest = {origin:"",marketId:0,exchangeId:0};
var myScrollIntoView = null;
var allScrollIntoView = null;
var popScrollIntoView = null;
var currentMenuEvent = 0;
var currentPageEvent = 0;
var currentMenuMarket = 0;
var currentPageMarket = 0;
var currentMenuExchange = 0;
var currentPageExchange = 0;
var nodeMap = new Array();
var nodeMapId = 0;

function initialiseScript() {
	processMappings();
};

// ********** INITIALISE AND CREATE THE ALL MARKETS MENU FUNCTION **********
function createAllMarketMenu() {
	if (!fCustomiseFlag) {
	    splitSkeleton('popular');
		document.getElementById("popularSportsTreeContainer").innerHTML = getMenuItems("popularSkeletonArray",0,0,"popularSportsTreeContainer","popularPathArray","popularParents1");
	}
	splitSkeleton('all');
	meatArray.length = 0
	splitSkeleton('meat');
	document.getElementById("allSportsTreeContainer").innerHTML = getMenuItems("allSkeletonArray",0,0,"allSportsTreeContainer","allPathArray","menuParents1");
	primeBrowserCache();
}

// ********** INITIALISE AND CREATE THE MY MARKETS MENU FUNCTION **********
function createMyMarketMenu() {
	if (myMarketsString != "") {
		integrateMyEventsAndMarkets();
	}
	splitSkeleton('my');
	domWrite('myMarketsTreeContainer',getMenuItems("mySkeletonArray",0,0,"myMarketsTreeContainer","myPathArray","myMarketsParents1",null,true));
}

// Move predetermined folders / events to the end
function doMoveToEnd(arr) {
	var movedElementsArray = new Array();
	for (var i = 0; i < arr.length; i++) {
		var node = arr[i];
		var id = node[ID];
		
		// remove from current position
		for (var iCountX = 0; iCountX < moveToEndOfParentMapping.length; iCountX++) {
			if (id == moveToEndOfParentMapping[iCountX]) {
				movedElementsArray[movedElementsArray.length] = node;
				arr.splice(i, 1);
				break;
			}
		}
	}
		
	// move to the end
    for (var i = 0; i < movedElementsArray.length; i++) {
		arr.push(movedElementsArray[i]);
    }
}

function mapCompetition(node) {
	var id = node[ID]
	for (var iCountX = 0; iCountX < externalZoneMapping.length; iCountX = iCountX+2) {
		if (externalZoneMapping[iCountX] == id) {
			node[COMPETITION_ID] = externalZoneMapping[iCountX];
			break;
		}
	}
	if (node[COMPETITION_ID] == COMPETITION_ID_NULL) node[COMPETITION_ID] = 0;
}


// ********** SPLIT THE SKELETON STRINGS INTO THE SKELETON ARRAYS **********
function splitSkeleton(sWhichArray, sPrefix) {
	switch (sWhichArray) {
        case "popular" :
            for (var iCountY = 0; iCountY < toteZoneMapping.length; iCountY = iCountY+3) {
                popularString+="0~-1~" + toteZoneMapping[iCountY] + "~" + toteZoneMapping[iCountY+1] + "~0~-1|";
            }
            var arr = popularString.substring(0,popularString.length-1).split("|");
            for (var i = 0; i < arr.length; i++) {
				var node = arr[i].split("~");
				if (!fCustomiseFlag || getIsOriginal(node)) {
					var key = node[PARENT_ID] + "." + node[ID] + "." + getExchangeId(node) + "." + getIsShortcut(node);
					if (!popularSkeletonMap[key]) {
						//popularSkeletonMap[key] = node;
						popularSkeletonArray.push(node);
						mapCompetition(node);
					}
				}
            }
            doMoveToEnd(popularSkeletonArray);
        break;
        case "all" :
            for (var iCountY = 0; iCountY < toteZoneMapping.length; iCountY = iCountY+3) {
                skeletonString+="0~-1~" + toteZoneMapping[iCountY] + "~" + toteZoneMapping[iCountY+1] + "~0~-1|";
            }
            var arr = skeletonString.substring(0,skeletonString.length-1).split("|");
            for (var i = 0; i < arr.length; i++) {
				var node = arr[i].split("~");
				if (!fCustomiseFlag || getIsOriginal(node)) {
					var key = node[PARENT_ID] + "." + node[ID] + "." + getExchangeId(node) + "." + getIsShortcut(node);
					if (!allSkeletonMap[key]) {
						//allSkeletonMap[key] = node;
						allSkeletonArray.push(node);
						mapCompetition(node);
					}
				}
            }
            doMoveToEnd(allSkeletonArray);
        break;
		case "my" :
			if (mySkeletonString!="") {
				if (mySkeletonString.substring(mySkeletonString.length-1,mySkeletonString.length)=="|") {
					mySkeletonArray = mySkeletonString.substring(0,mySkeletonString.length-1).split("|");
				}
				else {
					mySkeletonArray = mySkeletonString.split("|");				
				}
				for (var i = 0; i < mySkeletonArray.length; i++) {
					var node = mySkeletonArray[i].split("~");
					mySkeletonArray[i] = node;
					mapCompetition(node);
				}
			}
			if (myMarketsString!="") {
				if (myMarketsString.substring(myMarketsString.length-1,myMarketsString.length)=="|") {
					myMarketsArray = myMarketsString.substring(0,myMarketsString.length-1).split("|");
				}
				else {
					myMarketsArray = myMarketsString.split("|");
				}
				for (var i = 0; i < myMarketsArray.length; i++) {
					var node = myMarketsArray[i].split("~");
					var id = node[ID];
					myMarketsArray[i] = node;
					mapCompetition(node);
				}
			}
		break;
		case "meat" :
			if (typeof sPrefix == "undefined") sPrefix = "";

			// Load up initial string of markets / coupons
			if (meatArray.length == 0) {
				meatMap = new Array();
				var oTempArray = myMeatString.split("|");
				for (var i = 0; i < oTempArray.length; i++) {
					var sNode = oTempArray[i]
					if (sNode != "") {
						node = sNode.split("~");
						if (getIsOriginal(node)) { // no shortcuts or aliases through this way for the moment
							var key = node[PARENT_ID] + "." + node[ID] + "." + getExchangeId(node) + "." + getIsShortcut(node);
							if (!meatMap[key]) {
								meatArray.push(node);
								//meatMap[key] = node;
							}
						}
					}
				}
			}

			if (m_meatArray!="") {
				var oTempArray = m_meatArray.split("|");
				for (var i = 0; i < oTempArray.length; i++) {
					var sNode = oTempArray[i]
					if (sNode != "") {
						sNode = sPrefix + sNode;
						node = sNode.split("~");
						if (getIsOriginal(node)) { // no shortcuts or aliases through this way for the moment
							var key = node[PARENT_ID] + "." + node[ID] + "." + getExchangeId(node) + "." + getIsShortcut(node);
							if (!meatMap[key]) {
								meatArray.push(node);
								//meatMap[key] = node;
								myMeatString += "|" + sNode;
							}
						}
					}
				}
			}

		break;
	}
	synchroniseCache();
}

// ********** SYNCHRONISE THE MEAT **********
function synchroniseCache() {
	if (oPointerHandle) {
		if (!oPointerHandle.closed) {
			if (oPointerHandle.myMeatString) {
				if (oPointerHandle.myMeatString != myMeatString) {
					oPointerHandle.myMeatString = myMeatString;
					oPointerHandle.meatArray.length = 0;
					oPointerHandle.splitSkeleton('meat');
				}
			}
		}
		else {
			oPointerHandle = null;
		}
	}
}

// ********** PAINT TO SCREEN FUNCTION **********
function domWrite(layerName,sHTML,bForceRefresh) {
	document.getElementById(layerName).innerHTML = "";
	document.getElementById(layerName).innerHTML = sHTML;
	if (sHTML == "") {
		document.getElementById(layerName).style.display = "none";
	}
	else {
		document.getElementById(layerName).style.display = "";
	}
    if (typeof(fixMozillaHeights) == "function") {// && layerName.indexOf("menuParents") == -1) {
		fixMozillaHeights();
	}
}

// ********** INSERTS THE MARKETS FROM THE MY MARKET STRING INTO THE CORRECT PLACE IN THE MYSKELETON STRING BECAUSE THE MYSKELETON IS THE ONLY ONE THAT CAN CONTAIN MEAT AS WELL AS SKELETON **********
function integrateMyEventsAndMarkets() {
	splitSkeleton('my');
	var myUberArray = new Array();
	for (var iCounterX=0; iCounterX<mySkeletonArray.length; iCounterX++) {
		myUberArray[myUberArray.length]=mySkeletonArray[iCounterX];
		for (iCounterW=0; iCounterW<myMarketsArray.length; iCounterW++) {
			if (mySkeletonArray[iCounterX][ID]==myMarketsArray[iCounterW][PARENT_ID]) {
				myUberArray[myUberArray.length]=myMarketsArray[iCounterW];
				myUberArray[myUberArray.length-1][HAS_MARKET]="2";
			}
		}
	}
var oTempArray=new Array(myUberArray.length);
	for (iCounterY=0; iCounterY<myUberArray.length; iCounterY++) {
		oTempArray[iCounterY]=myUberArray[iCounterY].join("~");
	}
	mySkeletonString=oTempArray.join("|");
}

function flushMeatCache() {
	if (!fCustomiseFlag) {
		meatArray.length = 0;
		myMeatString = myMarketsString;
		splitSkeleton('meat');
		oFlushMeatTimeout = setTimeout("flushMeatCache()", iFlushMeatCacheTimer*1000);
	}
}

// ********** FUNCTION THAT IS CALLED EVERY TIME AN EVENT IS CLICKED OR WHENEVER THE MEAT ARRAY HAS FINISHED LOADING **********
// writeItems('allSkeletonArray',982477,'allSportsTreeContainer','allPathArray','menuParents1','true')
function writeItems(strArrayName,parentID,grandparentID,layerName,strMenuPathArrayName,menuPathLayer,bAddToMenuPath,sOrigin,homePath) {
	if (homePath && homePath != "") {
		goMenu(grandparentID,parentID,0);
	}
	switch (layerName) {
		case "allSportsTreeContainer" :
			currentAllItem = parentID;
			document.getElementById("allSportsTreeContainer").scrollTop = 0;
		break;
		case "popularSportsTreeContainer" :
			currentPopItem = parentID;
			document.getElementById("popularSportsTreeContainer").scrollTop = 0;
		break;
		case "myMarketsTreeContainer" :
			currentMyItem = parentID;
			document.getElementById("myMarketsTreeContainer").scrollTop = 0;
		break;
	}
	
	domWrite(layerName,getMenuItems(strArrayName,parentID,grandparentID,layerName,strMenuPathArrayName,menuPathLayer,bAddToMenuPath),sOrigin);
	
	if (typeof(adjustHeights) != "undefined") {
		adjustHeights();
	}
	
	currentMenuEvent = parentID;
	
}

//Loads the default layoutTemplateMapping for an event, if one exists.
function loadDefaultMappingForEvent(eventId, origin) {
	var templateMapping = getDefaultMappingForEvent(eventId, origin);
	if (templateMapping) {
		loadContentAreaForMapping(eventId, templateMapping, origin);
	}
}
function getDefaultMappingForEvent(eventId, origin) {
	var templateMapping = null;
	var originStr = (origin) ? origin : "LHM";
	var bInternalRequest = (originStr.substr(0,3) == "LHM");
	if ((!bCMSNavTabRequest && !showZones) && bInternalRequest) return null;
	if (typeof(layoutTemplateList) == "undefined" || typeof(layoutTemplateMappingList) == "undefined") {
		return null;
	}
	processMappings();

	if (templateMappings[eventId]) {
		var templateMappingList = templateMappings[eventId], templateMapping;
	
		var i, len=templateMappingList.length;
		for (i=0; i<len; i++) {
			if (templateMappingList[i].defaultTemplate) {
				templateMapping = templateMappingList[i];
				break;
			}
		}
	}
	
	return templateMapping;
}


var mappingsProcessed = false;
var templates = {};
var templateMappings = {};
function processMappings() {
	if (!mappingsProcessed) {
		var i;
		for (i=0; i<layoutTemplateList.length; i++) {
			templates[layoutTemplateList[i].id] = layoutTemplateList[i];
		}
		
		for (i=0; i<layoutTemplateMappingList.length; i++) {
			
			if (!templateMappings[layoutTemplateMappingList[i].mappingId]) {
				templateMappings[layoutTemplateMappingList[i].mappingId] = [];
			}
			
			templateMappings[layoutTemplateMappingList[i].mappingId].push(layoutTemplateMappingList[i]);
		}
		mappingsProcessed = true;	
	}
}

function loadContentAreaForMapping(eventId, templateMapping, origin) {
	var contentFrame = window.parent.frames.main;
	
	var template = templates[templateMapping.layoutTemplateId];
	if (template.URL.indexOf("http") == 0) {
		templateUrl = template.URL;
	}
	else {
		templateUrl = "http://" + top.dMgr.getApplication("widgets").contentDomain + template.URL;
	}
	
	var url = templateUrl + getParams();
	url += "#id=" + eventId;
	var sportId = getSportId(allSkeletonArray, eventId);
	url += "&ti=" + sportId;
	if (templateMapping.groupId) {
		url += "&groupId=" + templateMapping.groupId;
	}
	url +=  "&origin=" + origin;
	contentFrame.location = url;
}

function getParams() {
	return "?" + widgetUrlParams;
}

// ********** FUNCTION THAT IS CALLED EVERY TIME AN EVENT IS CLICKED OR WHENEVER THE MEAT ARRAY HAS FINISHED LOADING **********
function getMenuItems(strArrayName,iParentID,iGrandparentID,layerName,strMenuPathArrayName,menuPathLayer,bAddToMenuPath,bMyBets,isAsyncLoad) {
	if (!isAsyncLoad || nodeMapId != iParentID) {
		nodeMap = new Array();
		nodeMapId = iParentID;
	}
	var key = ""
	oAddAllArray = new Array(0);
	var bHasMarket = false; 
	aHTML = [];
	if (iParentID != 0 && layerName == "myMarketsTreeContainer") {
		strArrayName = "allSkeletonArray";
	}
	arrayName = eval(strArrayName);
	iParentID = iParentID;
	aHTML.push(sContainerArray[1]);
	for (var i=0;i<arrayName.length;i++) {
		var node = arrayName[i];
		var key = node[PARENT_ID] + "." + node[ID] + "." + getExchangeId(node) + "." + getIsShortcut(node);
		if (!nodeMap[key]) {
			nodeMap[key] = node;

			// build menu path
			if (node[ID]==iParentID && bAddToMenuPath == "true") {
				if (layerName == "myMarketsTreeContainer") {
					addToMenuPath(i,arrayName,strArrayName,strMenuPathArrayName,menuPathLayer,layerName);
				}
				else {
					if (iGrandparentID == null || node[PARENT_ID]==iGrandparentID) {
						addToMenuPath(i,arrayName,strArrayName,strMenuPathArrayName,menuPathLayer,layerName);
					}
				}
			}

			// Add my markets meat at top level
			if (layerName == "myMarketsTreeContainer" && iParentID==0) {
				if (node[HAS_MARKET] == 2) {
					for (var inew=0;inew<meatArray.length;inew++) {
						if ((meatArray[inew][ID]==node[ID]) && (meatArray[inew][EXCHANGE_ID]==node[EXCHANGE_ID])) {
							aHTML.push(buildMarketsHTML('meatArray',inew,layerName,strMenuPathArrayName,menuPathLayer,bMyBets));
							break;
						}
					}
				}
				else {
					aHTML.push(buildEventsHTML(strArrayName,i,layerName,strMenuPathArrayName,menuPathLayer));
				}
			}
				
			// build menu list
			if (node[PARENT_ID] == iParentID) {
				if (layerName == "myMarketsTreeContainer") {
					if (iParentID != 0) {
						aHTML.push(buildEventsHTML(strArrayName,i,layerName,strMenuPathArrayName,menuPathLayer));
					}		
				}
				else {
					if (node[NODE_TYPE] == 0 || getIsShortcut(node)) {
						aHTML.push(buildEventsHTML(strArrayName,i,layerName,strMenuPathArrayName,menuPathLayer));
					}
					else {
						aHTML.push(buildMarketsHTML(strArrayName,i,layerName,strMenuPathArrayName,menuPathLayer,null));
					}
				}
			}
				
			// Does this node have markets to load		
			if (node[ID] == iParentID && iParentID !=0) {
				if (node[HAS_MARKET] == 1){
					bHasMarket = true;
				}
			}
		}	
	}
	
	if (bHasMarket) {
		var pathArrayName = eval(strMenuPathArrayName);
		if (pathArrayName.length>=2) {
			iCouponParentID = pathArrayName[pathArrayName.length-2][ID];
		}
		if (checkMeatCache(iParentID)) {
			for (var i=iStartupMeatCounter;i<meatArray.length;i++) {
				var node = meatArray[i];
				var key = node[PARENT_ID] + "." + node[ID] + "." + getExchangeId(node) + "." + getIsShortcut(node);
				if (meatArray[i][PARENT_ID]==iParentID) {
					if (!nodeMap[key]) {
						nodeMap[key] = node;
						aHTML.push(buildMarketsHTML('meatArray',i,layerName,strMenuPathArrayName,menuPathLayer,bMyBets));
					}
			 	}
			}
		}
		else {
			aHTML.push("\n<tr>\n<td colspan=\"3\" class=\"MenuItemLoading\">" + sLoadingText + "</td>\n</tr>\n");
			getSpecificItemFromServer(strArrayName,iParentID,layerName,strMenuPathArrayName,menuPathLayer,bAddToMenuPath);
		}
	}
	aHTML.push("<tr><td width=\"100%\"><img src=\"" + sSpacer + "\" height=\"1\" width=\"1\" border=\"0\"></td><td class=\"GlobalBackground\"><img src=\"" + sSpacer + "\" height=\"1\" width=\"1\" border=\"0\"></td><td class=\"GlobalBackground\"><img src=\"" + sSpacer + "\" height=\"1\" width=\"1\" border=\"0\"></td></tr>");
	aHTML.push(sContainerArray[2]);
	return aHTML.join("");
}
	
function addToMenuPath(iIdx,arrayName,strArrayName,strMenuPathArrayName,menuPathLayer,layerName){
	var menuPathArray = eval(strMenuPathArrayName);
	menuPathArray[menuPathArray.length] = new Array(arrayName[iIdx][NODE_TYPE],arrayName[iIdx][ID],arrayName[iIdx][PARENT_ID],arrayName[iIdx][NAME],arrayName[iIdx][COMPETITION_ID]);
	buildMenuPath(strArrayName,strMenuPathArrayName,menuPathLayer,layerName);
}

function buildMenuPath(strArrayName,strMenuPathArrayName,menuPathLayer,layerName){
	var menuPathArray = eval(strMenuPathArrayName);
	var strHTML = sContainerArray[1];
	if (fCustomiseFlag) {
		switch (layerName) {
			case "popularSportsTreeContainer" :
				strHTML += "<tr><td width=\"100%\">" + sEmptyPathArray +  "</td></tr><tr onclick=\"" + strMenuPathArrayName + ".length=0;domWrite('" + menuPathLayer + "',sEmptyPathArray);writeItems('" + strArrayName + "',0,0,'" + layerName + "','" + strMenuPathArrayName + "','" + menuPathLayer + "')\"\">\n<td width=\"100%\" class=\"MenuBorder\">\n<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\n<tr>\n<td class=\"MenuPath\" onmouseover=\"this.className='MenuPathOver';\" onmouseout=\"this.className='MenuPath';\" onMouseDown=\"this.className='MenuPathDown';\" onMouseUp=\"this.className='MenuPath';\" width=\"100%\">" + MENU_TITLE_POPULARSPORTS + "</td>\n</tr>\n</table>\n</td>\n</tr>"
				break;
			case "allSportsTreeContainer" :
				strHTML += "<tr><td width=\"100%\">" + sEmptyPathArray +  "</td></tr><tr onclick=\"" + strMenuPathArrayName + ".length=0;domWrite('" + menuPathLayer + "',sEmptyPathArray);writeItems('" + strArrayName + "',0,0,'" + layerName + "','" + strMenuPathArrayName + "','" + menuPathLayer + "')\"\">\n<td width=\"100%\" class=\"MenuBorder\">\n<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\n<tr>\n<td class=\"MenuPath\" onmouseover=\"this.className='MenuPathOver';\" onmouseout=\"this.className='MenuPath';\" onMouseDown=\"this.className='MenuPathDown';\" onMouseUp=\"this.className='MenuPath';\" width=\"100%\">" + MENU_TITLE_ALLSPORTS + "</td>\n</tr>\n</table>\n</td>\n</tr>"
				break;
			case "myMarketsTreeContainer" :
				strHTML += "<tr><td width=\"100%\">" + sEmptyPathArray +  "</td></tr><tr onclick=\"" + strMenuPathArrayName + ".length=0;domWrite('" + menuPathLayer + "',sEmptyPathArray);writeItems('" + strArrayName + "',0,0,'" + layerName + "','" + strMenuPathArrayName + "','" + menuPathLayer + "')\"\">\n<td width=\"100%\" class=\"MenuBorder\">\n<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\n<tr>\n<td class=\"MenuPath\" onmouseover=\"this.className='MenuPathOver';\" onmouseout=\"this.className='MenuPath';\" onMouseDown=\"this.className='MenuPathDown';\" onMouseUp=\"this.className='MenuPath';\" width=\"100%\">" + MENU_TITLE_MYMARKETS + "</td>\n</tr>\n</table>\n</td>\n</tr>\n"
				break;
		}
	}

	var origin = MENU_ORIGIN_DEFAULT;
	switch (layerName) {
		case "allSportsTreeContainer" :
			origin = MENU_ORIGIN_ALLSPORTS;
		break;
		case "popularSportsTreeContainer" :
			origin = MENU_ORIGIN_POPULARSPORTS;
		break;
		case "myMarketsTreeContainer" :
			origin = MENU_ORIGIN_MYMARKETS;
		break;
	}

	for (var i=0;i<menuPathArray.length;i++) {
		var sName = menuPathArray[i][NAME];
		var sCompetitionClick = "";
		var sCompetitionStyle = "";
		if (menuPathArray[i][MP_COMPETITION_ID] && menuPathArray[i][MP_COMPETITION_ID] != 0) {	
			if (menuPathArray[i][PARENT_ID] == 0) {
				sCompetitionClick = "goToToday('" + menuPathArray[i][MP_COMPETITION_ID] + "', '" + origin + "');";
				sCompetitionStyle = "style=\"cursor:pointer;\" ";
			} else {
				sCompetitionClick = "goToMatchPage('" + menuPathArray[i][MP_COMPETITION_ID] + "', '" + origin + "');";
				sCompetitionStyle = "style=\"cursor:pointer;\" ";
			}
		}
		else if (getDefaultMappingForEvent(menuPathArray[i][ID], origin)) {
			sCompetitionClick = "loadDefaultMappingForEvent(" + menuPathArray[i][ID] + ", '" + origin + "')";
			sCompetitionStyle = "style=\"cursor:pointer;\" ";
		}
		
		var homePath = null;
		if (menuPathArray[i][NODE_TYPE] == 0) {
			homePath = menuPathArray[i][HOME_PATH];
		}
		else {
			homePath = menuPathArray[i][M_HOME_PATH];
		}

		if (i == menuPathArray.length-1) {
			strHTML += "<tr " + sCompetitionStyle + "onClick=\"" + strMenuPathArrayName + ".length=" + (i+1) + ";buildMenuPath('" + strArrayName + "','" + strMenuPathArrayName + "','" + menuPathLayer + "','" + layerName + "');writeItems('" + strArrayName + "'," + menuPathArray[i][ID] + "," + menuPathArray[i][PARENT_ID] + ",'" + layerName + "','" + strMenuPathArrayName + "','" + menuPathLayer + "','false','" + homePath + "');" + sCompetitionClick + "\">\n<td width=\"100%\">\n<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\n<tr>\n<td class=\"MenuPathSelectedItem\" width=\"100%\"><span class=\"GlobalBoldedText\">" + sName + "</span></td>\n</tr>\n</table>\n</td>\n</tr>\n";
		}
		else {
			strHTML += "<tr onClick=\"" + strMenuPathArrayName + ".length=" + (i+1) + ";buildMenuPath('" + strArrayName + "','" + strMenuPathArrayName + "','" + menuPathLayer + "','" + layerName + "');writeItems('" + strArrayName + "'," + menuPathArray[i][ID] + "," + menuPathArray[i][PARENT_ID] + ",'" + layerName + "','" + strMenuPathArrayName + "','" + menuPathLayer + "','false','" + homePath + "');" + sCompetitionClick + "\">\n<td width=\"100%\" class=\"MenuBorder\">\n<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\n<tr>\n<td class=\"MenuPath\" onmouseover=\"this.className='MenuPathOver';\" onmouseout=\"this.className='MenuPath';\" onMouseDown=\"this.className='MenuPathDown';\" onMouseUp=\"this.className='MenuPath';\" width=\"100%\">" + sName + "</td>\n</tr>\n</table>\n</td>\n</tr>\n";
		}
	}
	strHTML += sContainerArray[2];
	domWrite(menuPathLayer,strHTML);
}

// function that builds the table row code for events which is written to the screen
function buildEventsHTML(strArrayName,iIdx,layerName,strMenuPathArrayName,menuPathLayer) {
	arrayNewTempName = eval(strArrayName);
	var node = arrayNewTempName[iIdx]
	var origin = MENU_ORIGIN_DEFAULT;

	switch (layerName) {
		case "allSportsTreeContainer" :
			origin = MENU_ORIGIN_ALLSPORTS;
		break;
		case "popularSportsTreeContainer" :
			origin = MENU_ORIGIN_POPULARSPORTS;
		break;
		case "myMarketsTreeContainer" :
			origin = MENU_ORIGIN_MYMARKETS;
		break;
	}

	var ID_HORSETODAYSCARD = 13;
	var ID_SOCCERFIXTURES = 14;

	if (sThisRegion != "AUS_NZL" || (sThisRegion == "AUS_NZL" && node[ID] != 315220 && node[ID] != 189929)) {
		var sName = node[NAME];
		var sCompetitionClick = "";
		var bExpandMenu = true;
		if (typeof(node[COMPETITION_ID]) != "undefined") {
			if (node[COMPETITION_ID] == -1) {
				// tote
				sCompetitionClick = "goToTote('" + node[PARENT_ID] + "');";
				bExpandMenu = false;
			}
			else if (node[COMPETITION_ID] != 0) {
				if (node[PARENT_ID] == 0) {
					// sport
					sCompetitionClick = "goToToday('" + node[COMPETITION_ID] + "', '" + origin + "');";
				} else {
					// event
					sCompetitionClick = "goToMatchPage('" + node[COMPETITION_ID] + "', '" + origin + "');";
				}
			}
			else {
				sCompetitionClick = "loadDefaultMappingForEvent(" + node[ID] + ", '" + origin + "')";
			}
		}

		if (node[NODE_TYPE]==0) {
			var exchangeId = -1;
			var ifaceId = -1;
			var homePath = node[HOME_PATH];
			var displayStyle = node[DISPLAY_STYLE];
		}
		else {
			var exchangeId = node[EXCHANGE_ID];
			var ifaceId = node[INTERFACE_ID];
			var homePath = node[M_HOME_PATH];
			var displayStyle = node[M_DISPLAY_STYLE];
			sCompetitionClick = "";
		}
		var isShortcut = false;
		var parentId = node[PARENT_ID];
		var sClassMod = "";
		if (homePath && homePath != "") {
			var isShortcut = true;
			sClassMod = "promoted";
			//homePath = unescape(homePath);
			var homePathArray = homePath.split("&#62;");
			if (homePathArray.length >= 2) {
				var parentId = homePathArray[homePathArray.length-2];
			}
		}
		if (displayStyle != "") {
			sClassMod = displayStyle;
		}
		var classMenuEvent = "MenuEvent MenuNode_" + sClassMod;
		var classMenuEventOver = "MenuEventOver MenuNodeOver_" + sClassMod;
		var classMenuEventDown = "MenuEventDown MenuNodeDown_" + sClassMod;
		if (!fCustomiseFlag) {
			var eventId = node[ID];
			var eleId = layerName.substr(0,3) + "_" + eventId;
			if (isShortcut) {
				eleId += "_sc";
			}
			else if (!getIsOriginal(node)) {
				eleId += "_a";
			}
			if (bExpandMenu) {
				return "<tr id='" + eleId + "' onclick=\"changeCMSNavTab('sports'," + node[ID] + "," + node[PARENT_ID] + ");handleClickEvent(" + isShortcut + ", " + exchangeId + ", " + node[ID] + "," + parentId + ",'" + node[NAME] + "','" + origin + "'," + ifaceId + ",'" + strArrayName + "','" + layerName + "','" + strMenuPathArrayName + "','" + menuPathLayer + "','" + node[ID] + "');" + sCompetitionClick + "\">\n<td id='t2_" + eleId + "' width=\"100%\" colspan=\"3\" class=\"MenuBorder\">\n<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\n<tr>\n<td id='td_" + eleId + "' class=\"" + classMenuEvent + "\" onmouseover=\"this.className='" + classMenuEventOver + "';\" onmouseout=\"this.className='" + classMenuEvent + "';\" onMouseDown=\"this.className='" + classMenuEventDown + "';\" onMouseUp=\"this.className='" + classMenuEvent + "';\" width=\"100%\">" + sName + "</td>\n</tr>\n</table>\n</td>\n</tr>\n";			
			} else {
				return "<tr id='" + eleId + "' onclick=\"changeCMSNavTab('sports'," + node[ID] + "," + node[PARENT_ID] + ");" + sCompetitionClick + "\">\n<td id='t2_" + eleId + "' width=\"100%\" colspan=\"3\" class=\"MenuBorder\">\n<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\n<tr>\n<td id='td_" + eleId + "' class=\"" + classMenuEvent + "\" onmouseover=\"this.className='" + classMenuEventOver + "';\" onmouseout=\"this.className='" + classMenuEvent + "';\" onMouseDown=\"this.className='" + classMenuEventDown + "';\" onMouseUp=\"this.className='" + classMenuEvent + "';\" width=\"100%\">" + sName + "</td>\n</tr>\n</table>\n</td>\n</tr>\n";
			}
		}
		else {
			oAddAllArray[oAddAllArray.length] = node[ID] + "," + node[PARENT_ID] + "," + node[NAME] + "," + node[HAS_MARKET];
			if (bExpandMenu) {
				return "<tr onclick=\"handleClickEvent(" + isShortcut + ", " + exchangeId + ", " + node[ID] + "," + parentId + ",'" + node[NAME] + "','" + origin + "'," + ifaceId + ",'" + strArrayName + "','" + layerName + "','" + strMenuPathArrayName + "','" + menuPathLayer + "');" + sCompetitionClick + "\">\n<td width=\"100%\" colspan=\"3\" class=\"MenuBorder\">\n<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\n<tr>\n<td class=\"" + classMenuEvent + "\" onmouseover=\"this.className='" + classMenuEventOver + "';\" onmouseout=\"this.className='" + classMenuEvent + "';\" onMouseDown=\"this.className='" + classMenuEventDown + "';\" onMouseUp=\"this.className='" + classMenuEvent + "';\" width=\"100%\">" + sName + "</td></tr>\n</table>\n</td><td class=\"MenuCustomiseArrow\" onMouseOver=\"this.className='MenuCustomiseArrowOver';\" onMouseOut=\"this.className='MenuCustomiseArrow';\" onClick=\"addToMyMarket('true','" + node[ID] + "','" + node[PARENT_ID] + "','" + node[NAME].replace("'","\\'") + "','" + node[HAS_MARKET] + "');\" width=\"17\" valign=\"middle\" align=\"center\" nowrap><img src=\"" + sCustomiseArrow + "\"></td>\n</tr>\n";
			} else {
				return "<tr onclick=\"" + sCompetitionClick + "\">\n<td width=\"100%\" colspan=\"3\" class=\"MenuBorder\">\n<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" width=\"100%\">\n<tr>\n<td class=\"" + classMenuEvent + "\" onmouseover=\"this.className='" + classMenuEventOver + "';\" onmouseout=\"this.className='" + classMenuEvent + "';\" onMouseDown=\"this.className='" + classMenuEventDown + "';\" onMouseUp=\"this.className='" + classMenuEvent + "';\" width=\"100%\">" + sName + "</td></tr>\n</table>\n</td><td class=\"MenuCustomiseArrow\" onMouseOver=\"this.className='MenuCustomiseArrowOver';\" onMouseOut=\"this.className='MenuCustomiseArrow';\" onClick=\"addToMyMarket('true','" + node[ID] + "','" + node[PARENT_ID] + "','" + node[NAME].replace("'","\\'") + "','" + node[HAS_MARKET] + "');\" width=\"17\" valign=\"middle\" align=\"center\" nowrap><img src=\"" + sCustomiseArrow + "\"></td>\n</tr>\n";
			}
		}
	}
}

function changeCMSNavTab(tabName, eventId, parentEventId) {
	try {
		if (tabName == "sports" && (parentEventId == 0 || parentEventId == null)) {
			if (eventId) {
				parent.parent.frames['header'].frames['Nav'].setTargets(eventId);	
			} else {
				parent.parent.frames['header'].frames['Nav'].setTargets("sports");				
			}
		} else if (tabName == "multiples") {
			parent.parent.frames['header'].frames['Nav'].setTargets("multiples");
		}
	} catch (e) {
	}
}

function goToTote(toteId){
	var parentFrame = parent.frames['main'];
	if (parentFrame && !parentFrame.bPartnerLinks) {
		var currentLoc = parentFrame.document.location;
		for (var iCountZ = 0; iCountZ < toteZoneMapping.length; iCountZ = iCountZ+3) {
			if (toteZoneMapping[iCountZ] == toteId) {
				var toteUrl = toteZoneMapping[iCountZ+2];
				break;
			}
		}
		if (currentLoc.href != toteUrl) {
			currentLoc.href = toteUrl;
		}
	}
}

function goToToday(tId, sOrigin){
	var originStr = (sOrigin) ? sOrigin : "LHM";
	var bInternalRequest = (originStr.substr(0,3) == "LHM");
	if ((!bCMSNavTabRequest && !showZones) && sOrigin && bInternalRequest) return;

	marketRequest = {origin:originStr, marketId:tId, exchangeId:1};

	var parentFrame = parent.frames['main'];
	
	if (parentFrame && !parentFrame.bPartnerLinks) {
		var currentLoc = parentFrame.document.location;
		var externalSite = false;
		for (var iCountX = 0; iCountX < externalZoneMapping.length; iCountX = iCountX+2) {
			if (externalZoneMapping[iCountX] == tId) {
				externalSite = true;
				if (currentLoc.href.indexOf(externalZoneMapping[iCountX+1]) == -1) {
					currentLoc.href = externalZoneMapping[iCountX+1] + '?origin=' + originStr;
				}
				highlightMarket(0,0);
				break;
			}
		}
		if (!externalSite) {
			if (currentLoc.href.indexOf("/sportToday.jsp") == -1) {
				var url = 
					"http://" + 
					top.dMgr.getApplication("widgets").contentDomain + 
					"/widgets/layouts/sportToday.jsp" + getParams() + "#id=" + tId + "&origin=" + sOrigin;
				
				currentLoc.href = url;
				highlightMarket(0,0);
			} else if (parentFrame.betex.util.Loader.controllers.zoneViewController.domain.marketGroup.sportId != tId) {
				parentFrame.betex.util.Loader.controllers.zoneViewController.loadNewZone(tId, sOrigin);
				highlightMarket(0,0);
			}
		}
	}
}

function goToMatchPage(tmId, sOrigin, productId){
	var originStr = (sOrigin) ? sOrigin : "LHM";
	var bInternalRequest = (originStr.substr(0,3) == "LHM");
	if ((!bCMSNavTabRequest && !showZones) && sOrigin && bInternalRequest) return;
	if (!productId) { productId = 1; }
	marketRequest = {origin:originStr, marketId:tmId, exchangeId:productId};

	var parentFrame = parent.frames['main'];
	
	if (parentFrame && !parentFrame.bPartnerLinks) {
		var currentLoc = parentFrame.document.location;
	
		if (currentLoc.href.indexOf("/matchView.jsp") == -1) {
			var url = 
				"http://" + 
				top.dMgr.getApplication("widgets").contentDomain + 
				"/widgets/layouts/matchView.jsp" + getParams() + 
				"#id=" + tmId + "&origin=" + sOrigin + "&ex="+productId;

			currentLoc.href = url;
			highlightMarket(0,0);
		} else if (parentFrame.betex.util.Loader.controllers.matchViewController.domain.marketGroup.eventId != tmId) {
			parentFrame.betex.util.Loader.controllers.matchViewController.loadNewMatch(tmId, sOrigin, productId);
			highlightMarket(0,0);
		}
	}
}

// function that builds the table row code for markets which is written to the screen
function buildMarketsHTML(strArrayName,iIdx,layerName,strMenuPathArrayName,menuPathLayer,bMyBets){
	var arrayTempName = eval(strArrayName);
	var origin = MENU_ORIGIN_DEFAULT;
	switch (layerName) {
		case "allSportsTreeContainer" :
			sLevelID="a";
			origin = MENU_ORIGIN_ALLSPORTS;
		break;
		case "popularSportsTreeContainer" :
			sLevelID="p";
			origin = MENU_ORIGIN_POPULARSPORTS;
		break;
		case "myMarketsTreeContainer" :
			sLevelID="m";
			origin = MENU_ORIGIN_MYMARKETS;
		break;
	}

	var node = arrayTempName[iIdx];

	var homePath = node[M_HOME_PATH];
	var isShortcut = false;
	var parentId = null;
	if (homePath && homePath != "") {
		var isShortcut = true;
		//homePath = unescape(homePath);
		var homePathArray = homePath.split("&#62;");
		if (homePathArray.length >= 2) {
			var parentId = homePathArray[homePathArray.length-2];
		}
	}

	var eleId = layerName.substr(0,3) + "_" + node[ID] + "." + node[EXCHANGE_ID];
	if (isShortcut) {
		eleId += "_sc";
	}
	else if (!getIsOriginal(node)) {
		eleId += "_a";
	}
	
	var sClassMod = "";
	if (isShortcut) {
		sClassMod = "promoted";
	}
	if (node[ID]==iCurrentMarketID && node[EXCHANGE_ID]==iCurrentExchangeID) {
		var sClassName = "MenuMarketDown MenuNodeDown_" + sClassMod;
	}
	else {
		var sClassName = "MenuMarket MenuNode_" + sClassMod;
	}

	var htmlBetType = "\n<td class=\"MenuIcon\" nowrap>" + getBetTypeIcon(node[MARKET_TYPE],node[INTERFACE_ID]) + "</td>";
	htmlBetType = "";
	if (!fCustomiseFlag) {
		if (getInPlayIcon(node[BET_DELAY]) != "") {
			return "<tr class=\"" + sClassName + "\" id=\"" + eleId + "\" name=\"" + sLevelID + node[ID] + "\" onmouseover=\"canHighlight(this,'over','" + sClassMod + "');\" onmouseout=\"canHighlight(this,'out','" + sClassMod + "');\" onmousedown=\"canHighlight(this,'down','" + sClassMod + "');\" onmouseup=\"canHighlight(this,'up','" + sClassMod + "');\" onclick=\"handleClickMarket(" + isShortcut + "," + node[EXCHANGE_ID] + ",'" + node[ID] + "'," + parentId + ",'"+ node[NAME] +"',"+((bMyBets)? "MENU_ORIGIN_MYMARKETS" : "'" + origin + "'")+",'" + node[INTERFACE_ID] + "')\">\n<td colspan=\"2\" width=\"100%\" class=\"MenuBottomBorder\">" + node[NAME] + "</td>\n<td class=\"MenuBottomBorder\" nowrap>" + getInPlayIcon(node[BET_DELAY]) + "</td>" + htmlBetType + "\n</tr>\n";	
		}
		else {
			return "<tr class=\"" + sClassName + "\" id=\"" + eleId + "\" name=\"" + sLevelID + node[ID] + "\" onmouseover=\"canHighlight(this,'over','" + sClassMod + "');\" onmouseout=\"canHighlight(this,'out','" + sClassMod + "');\" onmousedown=\"canHighlight(this,'down','" + sClassMod + "');\" onmouseup=\"canHighlight(this,'up','" + sClassMod + "');\" onclick=\"handleClickMarket(" + isShortcut + "," + node[EXCHANGE_ID] + ",'" + node[ID] + "'," + parentId + ",'"+ node[NAME] +"',"+((bMyBets)? "MENU_ORIGIN_MYMARKETS" : "'" + origin + "'")+",'" + node[INTERFACE_ID] + "')\">\n<td colspan=\"3\" class=\"MenuBottomBorder\">" + node[NAME] + "</td>" + htmlBetType + "\n</tr>\n";	
		}
	}
	else {
		oAddAllArray[oAddAllArray.length] = node[ID] + "," + node[PARENT_ID] + "," + node[NAME] + ",2," + node[MARKET_TYPE] + "," + node[BET_DELAY] + "," + node[INTERFACE_ID] + "," + node[START_TIME] + "," + node[REGION_ID]; 
		if (getInPlayIcon(node[BET_DELAY]) != "") {
			return "<tr class=\"" + sClassName + "\" id=\"" + eleId + "\" name=\"" + sLevelID + node[ID] + "\" onmouseover=\"canHighlight(this,'over','" + sClassMod + "');\" onmouseout=\"canHighlight(this,'out','" + sClassMod + "');\" onmousedown=\"canHighlight(this,'down','" + sClassMod + "');\" onmouseup=\"canHighlight(this,'up','" + sClassMod + "');\">\n<td width=\"100%\" class=\"MenuBottomBorder\" colspan=\"2\">" + node[NAME] + "</td>\n<td class=\"MenuBottomBorder\" nowrap>" + getInPlayIcon(node[BET_DELAY]) + "</td>" + htmlBetType + "\n<td class=\"MenuCustomiseArrow\" onMouseOver=\"this.className='MenuCustomiseArrowOver';\" onMouseOut=\"this.className='MenuCustomiseArrow';\" onClick=\"addToMyMarket('true','" + node[ID] + "','" + node[PARENT_ID] + "','" + node[NAME].replace("'","\\'") + "','2','" + node[MARKET_TYPE] + "','" + node[BET_DELAY] + "','" + node[INTERFACE_ID] + "','" + node[START_TIME] + "','" + node[REGION_ID] + "'," + node[EXCHANGE_ID] + ");\" width=\"17\" valign=\"middle\" align=\"center\"><img src=\"" + sCustomiseArrow + "\"></a></td>\n</tr>\n";	
		}
		else {
			return "<tr class=\"" + sClassName + "\" id=\"" + eleId + "\" name=\"" + sLevelID + node[ID] + "\" onmouseover=\"canHighlight(this,'over','" + sClassMod + "');\" onmouseout=\"canHighlight(this,'out','" + sClassMod + "');\" onmousedown=\"canHighlight(this,'down','" + sClassMod + "');\" onmouseup=\"canHighlight(this,'up','" + sClassMod + "');\">\n<td width=\"100%\" class=\"MenuBottomBorder\" colspan=\"3\">" + node[NAME] + "</td>" + htmlBetType + "\n<td class=\"MenuCustomiseArrow\" onMouseOver=\"this.className='MenuCustomiseArrowOver';\" onMouseOut=\"this.className='MenuCustomiseArrow';\" onClick=\"addToMyMarket('true','" + node[ID] + "','" + node[PARENT_ID] + "','" + node[NAME].replace("'","\\'") + "','2','" + node[MARKET_TYPE] + "','" + node[BET_DELAY] + "','" + node[INTERFACE_ID] + "','" + node[START_TIME] + "','" + node[REGION_ID] + "'," + node[EXCHANGE_ID] + ");\" width=\"17\" valign=\"middle\" align=\"center\"><img src=\"" + sCustomiseArrow + "\"></a></td>\n</tr>\n";	
		}
		return "";
	}
}

function getIsShortcut(node) {
	var homePath = "";
	if (node[NODE_TYPE] == 0) {
		homePath = node[HOME_PATH];
	}
	else {
		homePath = node[M_HOME_PATH];
	}
	var isShortcut = false;
	if (homePath && homePath != "") {
		isShortcut = true;
	}
	return isShortcut;
}
function getIsOriginal(node) {
	return (node[NODE_TYPE] == 0 ? node[ALIAS] : node[M_ALIAS]) == 0;
}
function getExchangeId(node) {
	return node[NODE_TYPE] == 0 ? 0 : node[EXCHANGE_ID];
}

function canHighlight(oThisID,sEventType,sClassMod) {
	var temp1 = oThisID.id.split("_");
	var temp2 = temp1[1].split(".")
	var thisMarketId = temp2[0];
	var thisExchangeId = temp2[1];
	var isShortcut = temp1[2] == "sc";
	if ((iCurrentMarketID!=null && thisMarketId!=iCurrentMarketID) || iCurrentMarketID==null || fCustomiseFlag) {
		if (isShortcut) {
			className = "MenuEvent";
		}
		else {
			className = "MenuMarket";
		}
		switch (sEventType) {
			case "over" :
				oThisID.className = className + "Over MenuNodeOver_" + sClassMod;
			break;
			case "out" :
				oThisID.className = className + " MenuNode_" + sClassMod;
			break;
			case "down" :
				oThisID.className = className + "Down MenuNodeDown_" + sClassMod;
			break;
			case "up" :
				oThisID.className = className + " MenuNode_" + sClassMod;
			break;
		}
	}
}

// function that returns one of the following divs for each time of market type
function getBetTypeIcon(sBetType,iInterfaceID) {
	switch (sBetType) {
		case "O" :
			if (iInterfaceID == 3) {
				return sCouponsIcon;
			}
			else {
				return sOddsIcon;
			}
		break;   
		case "S" :
			return sRangeIcon;
		break; 
		case "L" :
			return sLineIcon;
		break; 
		case "A" :
			return sAsianHandicapIcon;
		break;
   }
}

// function that returns a string for the in play icon image based upon the bet delay time
function getInPlayIcon(iBetDelay) {
	if (iBetDelay > 0) {
		return "<img src='" + sSpacer + "' height='1' width='2' border='0'>" + sInplayIcon + "<img src='" + sSpacer + "' height='1' width='2' border='0'>";
	}
	else {
		return "";
	}
}

// function called by results returned from search query
function g(iMarketID, isCoupon, iExchangeID) {
	goMarket(iMarketID, ((isCoupon == "1") ? 1 : 3), false, 0, null, 'BSE', iExchangeID);
}

function handleClickMarket(isShortcut,iExchangeID,nodeId,parentId,nodeName,sOrigin,iId) {
	if (isShortcut) {
		//goMenu(parentId,iMarketID,1);
		sOrigin += "_SC";
		goMarket(nodeId,iId,null,null,nodeName,sOrigin,iExchangeID);
	}
	else {
		goMarket(nodeId,iId,null,null,nodeName,sOrigin,iExchangeID);
	}
}

function handleClickEvent(isShortcut,iExchangeID,nodeId,parentId,nodeName,sOrigin,iId,strArrayName,layerName,strMenuPathArrayName,menuPathLayer,homePath) {
	if (iExchangeID >= 0) {
		handleClickMarket(isShortcut,iExchangeID,nodeId,parentId,nodeName,sOrigin,iId);
		return;
	}
	if (isShortcut) {
		sOrigin += "_SC";
		marketRequest = {origin:sOrigin, marketId:0, exchangeId:1};
		goMenu(nodeId,0,1);
	}
	else {
		writeItems(strArrayName,nodeId,parentId,layerName,strMenuPathArrayName,menuPathLayer,'true');
	}
}

function goMarket(iMarketID,iId,bOutsideRequest,iOutsideParentID,sMarketName,sOrigin,iExchangeID,iBetSuperPartnerId, betString, tab) {
	/*
	 * Double clicking in FF fires two clicks and a double click
	 * we should only goMarket once... and sometimes clicks are
	 * lost (so we have the timeout to allow the user to select
	 * the last market they really clicked on
	 */
	if(iMarketID == currentlyShownMarketsId) {
		return;
	}
	currentlyShownMarketsId = iMarketID;
	if(currentlyShownMarketIdTimeout) {
		clearTimeout(currentlyShownMarketIdTimeout);
		currentlyShownMarketIdTimeout=null
	}
	currentlyShownMarketIdTimeout = setTimeout(function() {currentlyShownMarketsId=0;},250);
	try {
		if (bOutsideRequest) {
			iRequestFromOutsideParentID = iOutsideParentID;
		}

		highlightMarket(iMarketID,iExchangeID);
		if (!parent.frames.bCanTrade) {
			alert(sBrowserText);
			return false;
		}

		//This is only used in a partnerdeeplink.jsp page that partners use to generate links for their pages.
		if (parent.frames['main'] && parent.frames['main'].bPartnerLinks) {
			var indexType = "mi";
			if (iId == "3") {
				indexType = "ci";
			}
			path = sAbsolutePath + sPartnerPath + '?'+indexType+'='+iMarketID+'&rfr='+parent.frames['main'].iPartnerId+'&sid='+parent.frames['main'].iSID+'&origin=P_'+sOrigin+'&ex='+iExchangeID;
			parent.frames['main'].document.forms[0].ShowChild.value = path;
			parent.frames['main'].document.forms[0].ShowChildName.value = sMarketName;
			return;
		}

		var marketInterface = "mi";
		if (iId == 3) {
			marketInterface = "ci";
		}
		var marketShowing = isMarketShowing();

		//If it's the same market that's already showing, do the goMenu instead.
		if (marketShowing) {
			if (parent.frames['main'].interface_getUIControllerMarketId &&
					parent.frames['main'].interface_getUIControllerMarketId() == iMarketID &&
					parent.frames['main'].interface_getUIControllerExchangeId() == iExchangeID) {
				// Set the expected market request info so that goMenu() can determine the origin of the request
				// I use this convoluted method in order to not change any widget file at this time
				marketRequest = {origin:info.origin, marketId:info.marketId, exchangeId:info.exchangeId};

				goMenu(parent.frames['main'].interface_getUIControllerEventId(), parent.frames['main'].interface_getUIControllerMarketId(), iExchangeID);
				return;
			}
		}
	} catch (e) {
	}	
	

	if (marketInterface == "ci") {
		// Set the expected market request info so that goMenu() can determine the origin of the request
		// I use this convoluted method in order to not change any widget file at this time
		marketRequest = {origin:sOrigin, marketId:iMarketID, exchangeId:iExchangeID};

		if (parent.frames['main'] && parent.frames['main'].betexUIController) {
			loadMarketData(iMarketID, sOrigin, iExchangeID, "ci");
		} else {
			loadMarketFrame("ci=" + iMarketID + "&origin=" + sOrigin + "&ex=" + iExchangeID);
		}
	} else {
		// tournament tracking - bet super partner id
		var sBSPI = "";
		if (typeof iBetSuperPartnerId != "undefined" && iBetSuperPartnerId) {
			sBSPI = "&bspi=" + iBetSuperPartnerId;
		}
		var params = "mi=" + iMarketID + "&origin=" + sOrigin + "&ex=" + iExchangeID + sBSPI;
		if (typeof betString != "undefined") {
			params += "&bets=" + betString;
			window.BETEX_BETS_TO_PLACE = betString;
		} else {
			window.BETEX_BETS_TO_PLACE = null;
		}
		
		if (typeof tab != "undefined") {
			params += "&tab=" + tab;
		}
		
		marketFrameworkRequest.get(marketFrameworkURL + params);
	}
	return false;
}

function isMarketShowing() {
	var marketShowing = false;
	if (parent.frames['main'] && parent.frames['main'].betexUIController) {
		marketShowing = true;
	}

	//If it's a multiples frame we'll need to reload the whole frame
	if (checkForMultiples(parent.frames['main'].document.location.href)) {
		marketShowing = false;
	}
	
	if (parent.frames['main'].PageType && parent.frames['main'].PageType=="marketView") {
		marketShowing = true;
	}
	
	return marketShowing;
}

function handleMarketFrameworkResponse(info, requestId) {
	if (info) {
		// determine if the new or old market is a widget market type.
		var oldMarketIsWidgetsMarket = false;
		var newMarketIsWidgetsMarket = false;

		try {
			if (parent.frames['main'].PageType && parent.frames['main'].PageType=="marketView") {
				oldMarketIsWidgetsMarket = true;
			}
		}
		catch (e) {
		}
		newMarketIsWidgetsMarket = info.widgetMarket;
		
		// Set the expected market request info so that goMenu() can determine the origin of the request
		// I use this convoluted method in order to not change any widget file at this time
		marketRequest = {origin:info.origin, marketId:info.marketId, exchangeId:info.exchangeId};

		if (info.racecardMarket) {
			loadRacecardMarketFrame(info.marketId, info.origin, info.exchangeId, info.widgetUrl);
		} else if (oldMarketIsWidgetsMarket || newMarketIsWidgetsMarket) {
			if (newMarketIsWidgetsMarket) {
				loadWidgetMarketFrame(info.marketId, info.origin, info.exchangeId, info.widgetUrl);
			} else {
				loadMarketFrame("mi=" + info.marketId + "&origin=" + info.origin + "&ex=" + info.exchangeId);
			}
		} else if (parent.frames['main'] && parent.frames['main'].betexUIController) {
			loadMarketData(info.marketId, info.origin, info.exchangeId, "mi");
		} else {
			loadMarketFrame("mi=" + info.marketId + "&origin=" + info.origin + "&ex=" + info.exchangeId);
		}
	}
}

function handleMarketFrameworkTimeout(requestId) {
	
}

function loadMarketFrame(reqParams) {
	parent.frames['main'].location = "http://" + top.dMgr.getApplication("sports").constructContentUrl("betting/MarketView.do?") + reqParams;
}

function loadMarketData(iMarketID, sOrigin, iExchangeID, marketInterface) {
	if (marketInterface == "mi") {
		parent.frames['main'].interface_getMarketData("mi=" + iMarketID, sOrigin, iExchangeID, true);
	} else {
		parent.frames['main'].interface_getMarketData("ci=" + iMarketID, sOrigin, iExchangeID);
	}
}

function loadRacecardMarketFrame(iMarketID, sOrigin, iExchangeID, sUrl) {
	try {
		var app = parent.frames['main'];
		if (app.PageType && app.PageType=="racecard") {
			app.interface_LoadNewMarket(iMarketID, iExchangeID, sOrigin); //#tjp - may not be required
		}else{
			app.location = sUrl.replace(/&amp;/g, "&");
		}
	}
	catch (e) { 
		app.location = sUrl.replace(/&amp;/g, "&");
	}
}

function loadWidgetMarketFrame(iMarketID, sOrigin, iExchangeID, sUrl) {
	try {
		var appWidgets = parent.frames['main'];
		if (appWidgets.PageType && appWidgets.PageType=="marketView") {
			setWidgetDataSources(betexProducts)
			appWidgets.betex.util.Loader.controllers.marketViewController.loadNewMarket(iMarketID, sOrigin, iExchangeID);
		}else{
			appWidgets.location = sUrl.replace(/&amp;/g, "&");
		}
	}
	catch (e) { 
		appWidgets.location = sUrl.replace(/&amp;/g, "&");
	}
}

function setWidgetDataSources(inBetexProducts) {
	var appWidgets = parent.frames['main'];
	if (appWidgets.WidgetLayout) {
		// Save the mappings of the exchange id with the corresponding exchange URL
		if (inBetexProducts[1]) {
			appWidgets.betex.util.DataManager.dataSources[1] = inBetexProducts[1].getDomain();
		}
		if (inBetexProducts[2]) {
			appWidgets.betex.util.DataManager.dataSources[2] = inBetexProducts[2].getDomain();
		}
		
	}
}

function highlightMarket(iMarketID,iExchangeID) {
	if (iCurrentMarketID!=iMarketID || iCurrentExchangeID!=iExchangeID ) {
		if (iCurrentMarketID!=null) {
			if (document.getElementById("all_" + iCurrentMarketID + "." + iCurrentExchangeID)) {
				document.getElementById("all_" + iCurrentMarketID + "." + iCurrentExchangeID).className = "MenuMarket";
			}
			if (document.getElementById("pop_" + iCurrentMarketID + "." + iCurrentExchangeID)) {
				document.getElementById("pop_" + iCurrentMarketID + "." + iCurrentExchangeID).className = "MenuMarket";
			}
			if (document.getElementById("myM_" + iCurrentMarketID + "." + iCurrentExchangeID)) {
				document.getElementById("myM_" + iCurrentMarketID + "." + iCurrentExchangeID).className = "MenuMarket";
			}
		}
		if (document.getElementById("all_" + iMarketID + "." + iExchangeID)) {
			document.getElementById("all_" + iMarketID + "." + iExchangeID).className = "MenuMarketDown";
		}
		if (document.getElementById("pop_" + iMarketID + "." + iExchangeID)) {
			document.getElementById("pop_" + iMarketID + "." + iExchangeID).className = "MenuMarketDown";
		}
		if (document.getElementById("myM_" + iMarketID + "." + iExchangeID)) {
			document.getElementById("myM_" + iMarketID + "." + iExchangeID).className = "MenuMarketDown";
		}
		iCurrentMarketID = iMarketID;
		iCurrentExchangeID = iExchangeID;
	}
}

function goMenu(iEventID,iMarketID,iExchangeID,iOrigin) {
	// Infer which origin caused this call
	var origin = iOrigin || marketRequest.origin;
	if (iMarketID == null) { // gotoToday or gotoMatch
		if (marketRequest.marketId != iEventID) {
			origin = "";
		}	
	}
	else if (marketRequest.marketId != iMarketID || marketRequest.exchangeId != iExchangeID) {
		origin = "";
	}
	var bShortcutRequest = origin.indexOf("_SC") >= 0;
	var bZoneRequest =  origin.indexOf("_ZONE") >= 0;
	origin = origin.split()[0];
	var bInternalRequest = (origin.substr(0,3) == "LHM");
	marketRequest = {origin:"",marketId:0,exchangeId:0};
	if (iRequestFromOutsideParentID && iRequestFromOutsideParentID!=null && iRequestFromOutsideParentID!="") {
		iEventID = iRequestFromOutsideParentID;
	}
	if (!iEventID || iEventID == "") iEventID = 0;
	

	// Attempt to get an event id if it is missing
	// This is necessary for unfolding a coupon - coupon meat is preloaded.
	if (iEventID == 0 && iMarketID != null) {
		for (var i=0; i<meatArray.length; i++) {
			var node = meatArray[i]
			if (!getIsShortcut(node)) {
				if (node[ID]==iMarketID && node[EXCHANGE_ID]==iExchangeID) {
					iEventID = node[PARENT_ID];
					break;
				}
			}
		}
	}
	

	if(iMarketID){
		highlightMarket(iMarketID,iExchangeID);
	}
	
	if (currentMenuId == MENUD_ID_MYMARKETS && !bInternalRequest) {
		if (parent.frames['main']) {
			if (parent.frames['main'].betexUIController) {
				if (parent.frames['main'].betexUIController.marketViewController) {
					if (parent.frames['main'].betexUIController.marketViewController.updatingMyMarkets) {
						parent.frames['main'].betexUIController.marketViewController.updatingMyMarkets = false;
						return;
					}
				}
			}
		}
	}
	
	if (!bShortcutRequest || bZoneRequest || iMarketID!=0) {
		currentPageEvent = iEventID;
	}

	// If the market is unchanged then don't go to it even if the parent event is different
	if (currentMenuMarket == iMarketID && currentMenuExchange == iExchangeID && origin == "") {
		return;
	}
	currentMenuMarket = iMarketID;
	currentMenuExchange = iExchangeID;

	// If the event is unchanged then don't go to it
	if (currentMenuEvent == iEventID) {
		return;
	}
	currentMenuEvent = iEventID;

	var bFoundMarket = false;
	
	// If this is a zone trying to go to a menu item after a shortcut has already gone to the correct item, then ignore
	if (bShortcutRequest && bZoneRequest) return;


	// Find the market in My Markets
	if (origin == MENU_ORIGIN_MYMARKETS && !bShortcutRequest) {
		bFoundMarket = true;
	}
	else if (bInternalRequest) {
		var sportId = getSportId(allSkeletonArray, currentMenuEvent);
		for (var iCounterA=0; iCounterA<mySkeletonArray.length; iCounterA++) {
			if (mySkeletonArray[iCounterA][ID]==sportId) {
				var iParentID = mySkeletonArray[iCounterA][PARENT_ID]
				if (!bFoundMarket && currentMenuId == MENUD_ID_MYMARKETS) {
				}
				else {
					// Reset to top of menu
					myPathArray.length = 0;
					//writeItems("emptySkeletonArray",iEventID,iParentID,"myMarketsTreeContainer","sEmptyPathArray","myMarketsParents1");
					//writeItems("mySkeletonArray",iEventID,iParentID,"myMarketsTreeContainer","myPathArray","myMarketsParents1");
					
					// bold
					var ele = document.getElementById("td_myM_" + sportId);
					if (ele) {
						myScrollIntoView = ele;
					}
				}

				break;
			}
		}
	}
	
	// Find the market in Popular Sports
	if (origin == MENU_ORIGIN_POPULARSPORTS && !bShortcutRequest) {
		bFoundMarket = true;
	}
	else if(bInternalRequest) {
		for (var iCounterA=0; iCounterA<popularSkeletonArray.length; iCounterA++) {
			if (popularSkeletonArray[iCounterA][ID]==iEventID) {
				var node = popularSkeletonArray[iCounterA];
				if (getIsOriginal(node) && !getIsShortcut(node)) {
					var iParentID = node[PARENT_ID]
					if (!bFoundMarket && currentMenuId == MENUD_ID_POPULARSPORTS) {
						// Unfurl
						buildPath(popularSkeletonArray,iEventID);
						if ((popularPathArray.length==0) || (popularPathArray.length>0 && popularPathArray[popularPathArray.length-1][ID] != iEventID)) {
							writeItems("emptySkeletonArray",iEventID,iParentID,"popularSportsTreeContainer","sEmptyPathArray","popularParents1");
							popularPathArray.length = 0;
							for (var iCounterB=oPathArray.length-1;iCounterB>=0;iCounterB--) {
								addToMenuPath(oPathArray[iCounterB],popularSkeletonArray,"popularSkeletonArray","popularPathArray","popularParents1","popularSportsTreeContainer");
							}
							writeItems("popularSkeletonArray",iEventID,iParentID,"popularSportsTreeContainer","popularPathArray","popularParents1");
						}
						bFoundMarket = true;
					}
					else {
						// Reset to top of menu
						popularPathArray.length = 0;
						//writeItems("emptySkeletonArray",iEventID,iParentID,"popularSportsTreeContainer","sEmptyPathArray","popularParents1");
						//writeItems("popularSkeletonArray",iEventID,iParentID,"popularSportsTreeContainer","popularPathArray","popularParents1");
						
						// bold
						var sportId = getSportId(popularSkeletonArray, currentMenuEvent);
						var ele = document.getElementById("td_pop_" + sportId);
						if (ele) {
							popScrollIntoView = ele;
						}
					}

					break;
				}
			}
		}
	}
	
	// Find the market in All Sports
	if (origin == MENU_ORIGIN_ALLSPORTS && !bShortcutRequest) {
		bFoundMarket = true;
	}
	else {
		for (var iCounterA=0; iCounterA<allSkeletonArray.length; iCounterA++) {
			if (allSkeletonArray[iCounterA][ID]==iEventID) {
				var isOriginal = (allSkeletonArray[iCounterA][NODE_TYPE] == 0 ? allSkeletonArray[iCounterA][ALIAS] : allSkeletonArray[iCounterA][M_ALIAS]) == 0;
				if (isOriginal && !getIsShortcut(allSkeletonArray[iCounterA])) {
					var iParentID = allSkeletonArray[iCounterA][PARENT_ID]

					// Switch to menu
					if (!bFoundMarket && currentMenuId != MENUD_ID_ALLSPORTS) {
						toggleMenuVisibility(MENUD_ID_ALLSPORTS, true);
					}

					if (!bFoundMarket) {
						// Unfurl
						buildPath(allSkeletonArray,iEventID);
						if ((allPathArray.length==0) || (allPathArray.length>0 && allPathArray[allPathArray.length-1][ID] != iEventID)) {
							writeItems("emptySkeletonArray",iEventID,iParentID,"allSportsTreeContainer","sEmptyPathArray","menuParents1");
							allPathArray.length = 0;
							for (var iCounterB=oPathArray.length-1;iCounterB>=0;iCounterB--) {
								addToMenuPath(oPathArray[iCounterB],allSkeletonArray,"allSkeletonArray","allPathArray","menuParents1","allSportsTreeContainer");
							}
							writeItems("allSkeletonArray",iEventID,iParentID,"allSportsTreeContainer","allPathArray","menuParents1");
						}
						bFoundMarket = true;
					}
					else {
						// Reset to top of menu
						allPathArray.length = 0;
						//writeItems("emptySkeletonArray",iEventID,iParentID,"allSportsTreeContainer","sEmptyPathArray","menuParents1");
						//writeItems("allSkeletonArray",iEventID,iParentID,"allSportsTreeContainer","allPathArray","menuParents1");
						
						// bold
						var sportId = getSportId(allSkeletonArray, currentMenuEvent);
						var ele = document.getElementById("td_all_" + sportId);
						if (ele) {
							allScrollIntoView = ele;
						}
					}
					
					break;
				}
			}
		}
	}

	oPathArray.length = 0;
	oIDArray.length = 0;
	iRequestFromOutsideParentID = null;
	
	if (!bInternalRequest) {
		foldMenu(false);
	}
	
	scrollIntoView();
	
	// if this was a shortcut then open the corresponding zone page
	if (bFoundMarket && iMarketID == 0 && bShortcutRequest) {
		openZonePage(iEventID, origin);
	}
}

function openZonePage(iEventID, origin) {
	origin += "_ZONE";
	var skeletonArray = allSkeletonArray;
	var called = false;	
	while (iEventID != 0 && !called) {
		var iParentID = 0;

		for (var i=0; i<skeletonArray.length && !called; i++) {
			var node = skeletonArray[i];

			if (node[ID] == iEventID && !getIsShortcut(node) && getIsOriginal(node)) {
			
				if (node[COMPETITION_ID]) {
					if (node[COMPETITION_ID] > 0) {
						if (node[PARENT_ID] == 0) {
							// sport
							goToToday(node[COMPETITION_ID], origin );
							called = true;
						} else {
							// event
							goToMatchPage(node[COMPETITION_ID], origin);
							called = true;
						}
					} else if (node[COMPETITION_ID] == -1) {
						// tote
						goToTote(node[PARENT_ID]);
						called = true;
					}
					else {
						loadDefaultMappingForEvent(node[ID], origin);
						called = true;
					}
				}
				
				iParentID = node[PARENT_ID];
				break;
			}					
		}

		iEventID = iParentID;
	}
	
	return called;
}

function buildPath(oWhichArray, iEventID) {
	var iPath = iEventID;

	// XXXab: This could be  improved with a lut based on ID
	
	var iCounterC;

	oMyArrayName = eval(oWhichArray);

	var len = oMyArrayName.length;
	var len1 = len + 1;

	do {
		for (iCounterC=0; iCounterC<len1; iCounterC++) {
			if (iCounterC!=len) {
				var isOriginal = (oMyArrayName[iCounterC][NODE_TYPE] == 0 ? oMyArrayName[iCounterC][ALIAS] : oMyArrayName[iCounterC][M_ALIAS]) == 0;
				if (isOriginal && oMyArrayName[iCounterC][ID]==iPath) {
					oPathArray.push(iCounterC);
					oIDArray.push(oMyArrayName[iCounterC][ID]);
					iPath=oMyArrayName[iCounterC][PARENT_ID];
					iCounterC=-1;
					break;
				}
			}
			else {
				return;
			}
		}
	} while (iPath!=0);
	
	return;

}

function checkMeatCache(iParentID) {
	var bMarketIsInCache = false;
	if (iParentID == lastMeatLoadedParentId) bMarketIsInCache = true;
	if (!bMarketIsInCache) {
		for (var i = iStartupMeatCounter;i<meatArray.length;i++) {
			if (meatArray[i][PARENT_ID] == iParentID) {
				bMarketIsInCache = true;
				break;
			}
		}
	}
	return bMarketIsInCache;
}

// pull meat from the server using one of the hidden iframes and a .do request
function getSpecificItemFromServer(strArrayName,iParentID,layerName,strMenuPathArrayName,menuPathLayer) {
	var sURL = sLoadMenuNodesAction + "?sReturnPath=" + sPointerToSelf + "&method=getMenuEvents&menuNodeId="+iParentID+"&strArrayName=" + strArrayName + "&iParentID=" + iParentID + "&layerName=" + layerName + "&strMenuPathArrayName=" + strMenuPathArrayName + "&menuPathLayer=" + menuPathLayer + sRegion + sLocale;
	if (layerName == "myMarketsTreeContainer") {
		parent.frames['menuManager2'].location.replace(sURL);
	}
	else if (layerName == "allSportsTreeContainer") {
		parent.frames['menuManager3'].location.replace(sURL);
	}
	else if (layerName == "popularSportsTreeContainer") {
		parent.frames['menuManager3'].location.replace(sURL);
	}
}

// called when the meat has loaded
function meatLoaded(strArrayName,sParentID,layerName,strMenuPathArrayName,menuPathLayer) {
	lastMeatLoadedParentId = sParentID;
	if (m_meatArray!="") {
		splitSkeleton('meat', "1~");
		m_meatArray="";
		switch (layerName) {
			case "allSportsTreeContainer" :
				if (currentAllItem == sParentID) {
					writeItems(strArrayName,sParentID,null,layerName,strMenuPathArrayName,menuPathLayer,false);
				}
			break;
			case "popularSportsTreeContainer" :
				if (currentPopItem == sParentID) {
					writeItems(strArrayName,sParentID,null,layerName,strMenuPathArrayName,menuPathLayer,false);
				}
			break;
			case "myMarketsTreeContainer" :
				if (currentMyItem == sParentID) {
					writeItems(strArrayName,sParentID,null,layerName,strMenuPathArrayName,menuPathLayer,false);
				}
			break;
		}
	}
	else {
		//writeItems(strArrayName,iCouponParentID,null,layerName,strMenuPathArrayName,menuPathLayer,false);
		alert(sErrorText);
		iCouponParentID = null;
	}
}


function toggleMenuVisibility(menuId, keepOpen, dontLoadMain){
    switch(menuId){
        case MENUD_ID_SAME:
			menuId = currentMenuId;
        break;
        case MENUD_ID_BACK:
			menuId = prevMenuId;
        break;
    }
    var previousMenuEvent = currentMenuEvent;
    var previousPageEvent = currentPageEvent;
    
	var menuTitle = "";
    var el = document.getElementById(menuId);
    var display = (el && el.style.display == "none") ? "" : "none";
	var doToggle = false;
	var containerId = "";
	if (!isIntegrateMultiples) {
		document.getElementById("menuWrapper").style.display = "";
		document.getElementById("linkMultiples").style.display = "";
		document.getElementById("menuWrapperMultiples").style.display = "none";
		document.getElementById("linkSingles").style.display = "none";
	}
    
    switch(menuId){
        case MENUD_ID_ALLSPORTS:
			menuTitle = MENU_TITLE_ALLSPORTS;
			containerId = "allSportsTreeContainer";
			//if(menuId == currentMenuId && allPathArray.length != 0)
			{
				allScrollIntoView = null;
	            display = "";
				allPathArray.length = 0;
				domWrite("menuParents1",sEmptyPathArray);
				writeItems("allSkeletonArray",0,0,"allSportsTreeContainer","allPathArray","menuParents1");
					
				// bold
				var sportId = getSportId(allSkeletonArray, previousPageEvent);
				var ele = document.getElementById("td_all_" + sportId);
				if (ele) {
					allScrollIntoView = ele;
				}
			}
			if (currentMenuId == MENUD_ID_MULTIPLES && !dontLoadMain && previousMenuEvent == 0){	
				parent.frames['main'].location.href = singleMainUrl + getQueryStringInfo();
			}
			doToggle = true;
			SetCookie("", top.constants_OPEN_SUB_MENU, "A")
            break;
        case MENUD_ID_POPULARSPORTS:
			menuTitle = MENU_TITLE_POPULARSPORTS;
			containerId = "popularSportsTreeContainer";
			//if(menuId == currentMenuId && popularPathArray.length != 0)
			{
				popScrollIntoView = null;
				display = "";
				popularPathArray.length = 0;
				domWrite("popularParents1",sEmptyPathArray);
				writeItems("popularSkeletonArray",0,0,"popularSportsTreeContainer","popularPathArray","popularParents1");

				// bold
				var sportId = getSportId(popularSkeletonArray, previousPageEvent);
				var ele = document.getElementById("td_pop_" + sportId);
				if (ele) {
					popScrollIntoView = ele;
				}
			}
			if (currentMenuId == MENUD_ID_MULTIPLES && !dontLoadMain && previousMenuEvent == 0){				
				parent.frames['main'].location.href = singleMainUrl + getQueryStringInfo();
			}
			doToggle = true;
			SetCookie("", top.constants_OPEN_SUB_MENU, "P")
            break;
        case MENUD_ID_MULTIPLES:
			currentMenuEvent = 0;
			menuTitle = MENU_TITLE_MULTIPLES;
			containerId = "multiplesTreeContainer2";
			if(!frames['multiplesTreeContainer'].multiplesPathArray){
				frames['multiplesTreeContainer'].location.href = mutiplesMenuUrl;
			}
			else {
				var mFrame = frames['multiplesTreeContainer'];
				var eMMP1 = mFrame.document.getElementById("multiplesMenuParents1");
				if (menuId == currentMenuId && eMMP1 && eMMP1.style.display != "none") {
					display = "";
					mFrame.multiplesPathArray.length=0;
					mFrame.domWrite('multiplesMenuParents1',sEmptyPathArray);
					mFrame.writeItems('multiplesSkeletonArray',0,'multiplesTreeContainer1','multiplesPathArray','multiplesMenuParents1');
					eMMP1.style.display = "none";
				}
			}
			if (!isIntegrateMultiples) {
				document.getElementById("menuWrapper").style.display = "none";
				document.getElementById("linkMultiples").style.display = "none";
				document.getElementById("menuWrapperMultiples").style.display = "";
				document.getElementById("linkSingles").style.display = "";
				if (currentMenuId != MENUD_ID_MULTIPLES){
					document.getElementById("linkSinglesText").innerHTML = currentMenuTitle;
				}
			}
			if (currentMenuId != MENUD_ID_MULTIPLES && !dontLoadMain){
				parent.frames['main'].location.href = mutiplesHomeUrl + getQueryStringInfo();		
			}
			doToggle = true;
            break;
        case MENUD_ID_LASTSEARCH:
			menuTitle = MENU_TITLE_LASTSEARCH;
			containerId = "lastSearchTreeContainer";
			if (currentMenuId == MENUD_ID_MULTIPLES && !dontLoadMain){				
				parent.frames['main'].location.href = singleMainUrl + getQueryStringInfo();
			}
			doToggle = true;
			break
        case MENUD_ID_MYMARKETS:
			menuTitle = MENU_TITLE_MYMARKETS;
			containerId = "myMarketsTreeContainer";
			{
				myScrollIntoView = null;
	            display = "";
				myPathArray.length = 0;
				domWrite("myMarketsParents1",sEmptyPathArray);
				writeItems("mySkeletonArray",0,0,"myMarketsTreeContainer","myPathArray","myMarketsParents1");
				
				// bold
				var sportId = getSportId(allSkeletonArray, previousPageEvent);
				var ele = document.getElementById("td_myM_" + sportId);
				if (ele) {
					myScrollIntoView = ele;
				}
			}
			if (top.frames['header'] && top.interfaces_getCurrentUser() && interfaces_getUserState(top.interfaces_getCurrentUser())) {
				if (mySkeletonArray.length > 0) {
					if(menuId == currentMenuId && myPathArray.length != 0){
						display = "";
						myPathArray.length = 0;
						domWrite("myMarketsParents1",sEmptyPathArray);
						writeItems("mySkeletonArray",0,0,"myMarketsTreeContainer","myPathArray","myMarketsParents1");
					}
					if (currentMenuId == MENUD_ID_MULTIPLES && !dontLoadMain && previousMenuEvent == 0){				
						parent.frames['main'].location.href = singleMainUrl + getQueryStringInfo();
					}
					doToggle = true;
				}
				else if (display == "") {
					if (!keepOpen) CustomiseMenu();
					doToggle = false;
				}
			}
			else {
				alert(sLoginMessage);
				if (top.frames['header']) {
					top.frames['header'].document.forms[0].username.focus();
				}
			}
            break;
	}
	if (doToggle) {
		if (menuId != MENUD_ID_MYMARKETS || display == "none") {
			//CustomiseMenuClose();
		}
		if (currentMenuId != menuId || !keepOpen) {
			var menuIds = [ MENUD_ID_POPULARSPORTS,
							MENUD_ID_ALLSPORTS,
							MENUD_ID_MYMARKETS,
							MENUD_ID_MULTIPLES,
							MENUD_ID_LASTSEARCH ];
			for(var id in menuIds) {
				var element = document.getElementById(menuIds[id]);
				if(element) {
					element.style.display = "none";
				}
			}
			var container = document.getElementById(containerId);
			if (container) {
				container.style.height = "0px";
			}
			if (el) {el.style.display = display;}
		}
		else if (keepOpen) {
			if (el) {el.style.display = "";}
		}
		if(fixMozillaHeights) fixMozillaHeights();
		scrollIntoView();

		if (menuId != currentMenuId) {
			prevMenuId = currentMenuId;
			prevMenuTitle = currentMenuTitle;
		}
		currentMenuId = menuId;
		currentMenuTitle = menuTitle;

	}
}

function scrollIntoView()
{
	if (!bMenuFolded) {
		if (allScrollIntoView && document.getElementById(MENUD_ID_ALLSPORTS).style.display == "") {
			allScrollIntoView.scrollIntoView(true);
			allScrollIntoView.style.fontWeight = "bold";
			allScrollIntoView.style.color = "black";
			allScrollIntoView = null;
		}
		if (popScrollIntoView && document.getElementById(MENUD_ID_POPULARSPORTS).style.display == "") {
			popScrollIntoView.scrollIntoView(true);
			popScrollIntoView.style.fontWeight = "bold";
			popScrollIntoView.style.color = "black";
			popScrollIntoView = null;
		}
		if (myScrollIntoView && document.getElementById(MENUD_ID_MYMARKETS).style.display == "") {
			myScrollIntoView.scrollIntoView(true);
			myScrollIntoView.style.fontWeight = "bold";
			myScrollIntoView.style.color = "black";
			myScrollIntoView = null;
		}
	}
}


function goToSport(sportID){
	allPathArray.length=0;
	writeItems('allSkeletonArray',sportID,0,'allSportsTreeContainer','allPathArray','menuParents1','true');
}

try{
	interface_registerJSResource("menu");
}catch(x){
	
}

function primeBrowserCache() {
	if (typeof(CDNAttribs) == "undefined") {
		return;
	}
	
	try {
		var scriptURL = "";
	
		if (CDNAttribs.isCDN) {
			scriptURL += "http://" + CDNAttribs.URL;
		}
		else {
			if (top.dMgr) {
				scriptURL += "http://" + top.dMgr.getApplication("widgets").contentDomain;
			}
		}
		scriptURL += "/widgets/layouts/marketView.jsp" + getParams();
		cachePrimer.get(scriptURL);
	}
	catch(e) {
		alert(e.message);
	}
}

function getSportId(skeletonArray, eventId) {
	var parentId = 0;
	var sportId = eventId;
	while ((parentId = getParentId(skeletonArray, sportId)) != 0)
	{
		sportId = parentId;
	}
	return sportId;
}

function getParentId(skeletonArray, eventId) {
	for (var iCounterA=0; iCounterA<skeletonArray.length; iCounterA++) {
		var node = skeletonArray[iCounterA];
		if (node[ID]==eventId  && getIsOriginal(node)) {
			return node[PARENT_ID]
		}
	}
	return 0;
}

function handleCachePrimerResponse() {}
function handleCachePrimerTimeout() {}
