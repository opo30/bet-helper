<!--
var fCustomiseFlag = false;
var oPointerHandle = null;
var sPointerToSelf = "menu";
var mySkeletonStringTemp;
var myMarketsStringTemp;
var bCustomMarketsLoaded = false;
var iStartupMeatCounter = 0;
var bMenuFolded = false;
var loadNextHorseRaceFromMultiples = false;
var bMenuFolderMouseOver = false;


function menuFolderMouseOver() {
	document.getElementById("menuFolderBar").className = "MenuFolderBarMouseOver";
	switch(bMenuFolded) {
		case false :
			document.getElementById("menuFolderTab").style.display = "none";
			document.getElementById("menuFolderTabMouseOver").style.display = "";
		break;
		case true :
			document.getElementById("menuFolderTabClosed").style.display = "none";
			document.getElementById("menuFolderTabClosedMouseOver").style.display = "";
		break;
	}
	bMenuFolderMouseOver = true;
}

function menuFolderMouseOut() {
	document.getElementById("menuFolderBar").className = "MenuFolderBar";
	switch(bMenuFolded) {
		case false :
			document.getElementById("menuFolderTab").style.display = "";
			document.getElementById("menuFolderTabMouseOver").style.display = "none";
		break;
		case true :
			document.getElementById("menuFolderTabClosed").style.display = "";
			document.getElementById("menuFolderTabClosedMouseOver").style.display = "none";
		break;
	}
	bMenuFolderMouseOver = false;
}

function foldMenu(inDoFold) {
	if(bMenuLoaded) {
		var doFold = !bMenuFolded;
		if (typeof(inDoFold) != "undefined") doFold = inDoFold;
		switch(doFold) {
			case true :
				parent.frames.document.getElementById("menuColumn").cols = "8,*";
//				document.getElementById("menuBody").style.display = "none";
				bMenuFolded = true;
			break;
			case false :
				parent.frames.document.getElementById("menuColumn").cols = "180,*";
//				document.getElementById("menuBody").style.display = "";
				bMenuFolded = false;
			break;
		}

		switch(bMenuFolded) {
			case false :
				if (bMenuFolderMouseOver) {
					document.getElementById("menuFolderTab").style.display = "none";
					document.getElementById("menuFolderTabMouseOver").style.display = "";
				}
				else {
					document.getElementById("menuFolderTab").style.display = "";
					document.getElementById("menuFolderTabMouseOver").style.display = "none";
				}
				document.getElementById("menuFolderTabClosed").style.display = "none";
				document.getElementById("menuFolderTabClosedMouseOver").style.display = "none";
			break;
			case true :
				document.getElementById("menuFolderTab").style.display = "none";
				document.getElementById("menuFolderTabMouseOver").style.display = "none";
				if (bMenuFolderMouseOver) {
					document.getElementById("menuFolderTabClosed").style.display = "none";
					document.getElementById("menuFolderTabClosedMouseOver").style.display = "";
				}
				else {
					document.getElementById("menuFolderTabClosed").style.display = "";
					document.getElementById("menuFolderTabClosedMouseOver").style.display = "none";
				}
			break;
		}

		fixMozillaHeights();
	}
}

// This is here for compatibility
function switchTab(iTabID, dontLoadMain) {
	if(iTabID == 1) {
		toggleMenuVisibility(MENUD_ID_ALLSPORTS, true, dontLoadMain);
		foldMenu(false);
	}
	else if(iTabID == 2) {
		toggleMenuVisibility(MENUD_ID_MYMARKETS, true, dontLoadMain);
		foldMenu(false);
	}
	else if(iTabID == 3) {
		toggleMenuVisibility(MENUD_ID_MULTIPLES, true, dontLoadMain);
		foldMenu(false);
	}
	else if(iTabID == 4) {
		foldMenu(false);
	}
	else if(iTabID == 5) {
		toggleMenuVisibility(MENUD_ID_LASTSEARCH, true, dontLoadMain);
		foldMenu(false);
	}
}

function resetSearchResults() {
	document.getElementById('query').value = "";
	document.getElementById('searchResults').innerHTML = "";
	document.getElementById('resultsNumber').innerHTML = "";
	document.getElementById('resultsText').innerHTML = "";
	document.getElementById('searchNumberOfResultsWrapper').style.display = "none";
	document.getElementById('searchWelcome').style.display = "block";
	document.getElementById('searchResults').style.display = "none";
	document.getElementById('searchNoResults').style.display = "none";
	fixMozillaHeights();
}

function initialiseMyMarkets() {
	splitSkeleton('meat');
	if(top.interfaces_getCurrentUser && interfaces_getUserState(top.interfaces_getCurrentUser())) {	
		bCustomMarketsLoaded = true;
		createMyMarketMenu();
		iStartupMeatCounter = myMarketsArray.length;
		if (top.interfaces_getCurrentUser().preferences.displayMyMarketsDefault) {
			try {
				if(!checkForMultiples(parent.frames['main'].document.location.href)){
					if (currentMenuEvent == 0)
					{
						switchTab(2);
					}
				}
			} catch (ex) {
				// access denied error - menu sometimes loads before main - will only occur on changing tabs / loading sports.betfair
					if (currentMenuEvent == 0)
					{
						switchTab(2);
					}
			}
		}
	}
}

function checkIfMarketAlreadyLoaded() {
	try {
		var app = parent.frames['main'];
		if (app) {
		
			// General View properties
			if (app.interface_getProperties) {
				goMenu(app.interface_getProperties().eventId, app.interface_getProperties().marketId, app.interface_getProperties().exchangeId);
				return;
			}
			
			// Widget View
			if (app.interface_getUIController) {
				if (app.interface_getUIController()) {
					if (app.interface_getUIControllerMarket) {
						goMenu(app.interface_getUIControllerMarket().eventId,app.interface_getUIControllerMarket().marketId,app.interface_getUIControllerMarket().exchangeID);
						return;
					}
				}
			}
		}
	} catch (ex) {
	}

}

function CustomiseMenu() {
	if (top.interfaces_getCurrentUser() && interfaces_getUserState(top.interfaces_getCurrentUser())) {
		oPointerHandle = spawn('customiseMenu');
		toggleMenuVisibility(MENUD_ID_MYMARKETS, true);
	}
}

function CustomiseMenuClose() {
	if (top.interfaces_getCurrentUser() && interfaces_getUserState(top.interfaces_getCurrentUser())) {
		if (oPointerHandle) {
			if (!oPointerHandle.closed) {
				oPointerHandle.close();
			}
			oPointerHandle = null;
		}
	}
}

function NextSoccerMatch(){
	NextMarket(1);
}

function NextHorseRace() {
	NextMarket("13");
}

function NextMarket(eventType){
	//If the loaded frame is from Multiples, switch back to Single and load next horse race 	
	if(checkForMultiples(parent.frames['main'].document.location.href)){		
		loadNextMarketFromMultiples = true;	
		switchTab(1);			
		parent.frames['menuManager1'].document.location.replace(sSinglesDomain + "betting/LoadNextMarketRedirectAction.do?eventType=" + eventType);		
	} else {	
		parent.frames['menuManager1'].location.href= "/betting/LoadNextMarketRedirectAction.do?eventType=" + eventType;
	}
}

function fixMozillaHeights() {
	var containerHeight = 0;
	var px = 0;
	if (isMozilla()) {
		containerHeight = document.height;
		px = 0;
	}
	else {
		containerHeight = document.body.clientHeight;
		px = "px";
	}
	containerHeight -= 3;
	
    var bottomItemHeight = document.getElementById("bottom").offsetHeight;
	var searchHeaderHeight = ((document.getElementById("searchMarketsHeader")) ? document.getElementById("searchMarketsHeader").offsetHeight : 0);
    var popularSportsHeight = document.getElementById("popularSportsHeader").offsetHeight 
			+ document.getElementById("popularParents1").offsetHeight
			+ document.getElementById("popularParents2").offsetHeight
			+ document.getElementById("popularParents3").offsetHeight;
    var allSportsHeight = document.getElementById("allSportsHeader").offsetHeight 
			+ document.getElementById("menuParents1").offsetHeight
			+ document.getElementById("menuParents2").offsetHeight
			+ document.getElementById("menuParents3").offsetHeight;
    var myMarketsHeight = document.getElementById("myMarketsHeader").offsetHeight
			+ document.getElementById("myMarketsCustomise").offsetHeight
			+ document.getElementById("myMarketsParents1").offsetHeight
			+ document.getElementById("myMarketsParents2").offsetHeight
			+ document.getElementById("myMarketsParents3").offsetHeight;
    var multiplesHeight = (document.getElementById("multiplesHeader"))? document.getElementById("multiplesHeader").offsetHeight : 0;
    var lastSearchHeight = (document.getElementById("lastSearchHeader"))? document.getElementById("lastSearchHeader").offsetHeight : 0;

	var menus = [ {parent:MENUD_ID_POPULARSPORTS, container:"popularSportsTreeContainer"},
					{parent:MENUD_ID_ALLSPORTS, container:"allSportsTreeContainer"},
					{parent:MENUD_ID_MYMARKETS, container:"myMarketsTreeContainer"},
					{parent:MENUD_ID_MULTIPLES, container:"multiplesTreeContainer2"},
					{parent:MENUD_ID_LASTSEARCH, container:"lastSearchTreeContainer"} ];

	var marketsHeight = containerHeight -
						searchHeaderHeight -
						popularSportsHeight -
						allSportsHeight -
						myMarketsHeight -
						multiplesHeight  -
						lastSearchHeight -
						bottomItemHeight;
	if(marketsHeight < 0) marketsHeight = 0;

    for(var i in menus) {
        var parent = document.getElementById(menus[i].parent);
        var container = document.getElementById(menus[i].container);
        if(parent && container) {
            if(parent.style.display == "none") {
				container.style.height = 0 + px;
			} 
			else {
				container.style.height = marketsHeight + px;
			}
        }
    }

    document.getElementById("menuWrapper").style.height = (containerHeight - searchHeaderHeight - bottomItemHeight) + px;
    document.getElementById("menuBody").style.height = containerHeight + px;
    document.getElementById("menuLayoutTable").style.height = containerHeight + px;
    document.getElementById("menuContainerTable").style.height = containerHeight + px;
    document.getElementById("menuFolderBar").style.height = containerHeight + px;
}

function getQueryStringInfo() {
	var queryStringInfo = 
				"?timeZone=" +
				GetCookie("betexTimeZoneSessionCookie", "") +
				"&region=" +
				GetCookie("betexRegionSessionCookie", "") +
				"&locale=" +
				GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" +
				GetCookie("betexBrand", "") + 
				"&currency=" +
				GetCookie("betexCurrencySessionCookie", "");
	return queryStringInfo;			
}

try {
	interface_registerJSResource("frontpageMenu");
}
catch(x) {
	
}
//-->
