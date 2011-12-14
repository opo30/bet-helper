var controllers_PLACE_BETS = null;

var controllers_PLACE_BETS_TAB = 1;
var controllers_PLACE_MYBETS_TAB = 2;
var controllers_MARKET_INFORMATION_TAB = 3;
var controllers_HELP_TAB = 4;
var controllers_MINIGAMES_TAB = 5;

var controllers_PLACE_BETS_DEFAULT_VIEW = 1;
var controllers_PLACE_BETS_PLACEBETS_VIEW = 2;
var controllers_PLACE_BETS_VERIFY_VIEW = 3;
var controllers_PLACE_BETS_PROCESSING_VIEW = 4;
var controllers_PLACE_BETS_CONFIRMATION_VIEW = 5;

var controllers_MY_BETS_DEFAULT_VIEW = 1;
var controllers_MY_BETS_BET_VIEW = 2;
var controllers_My_BETS_PROCESSING_VIEW = 3;
var controllers_My_BETS_CONFIRMATION_VIEW = 4;
var controllers_My_BETS_MATCHED_BETS_HIDDEN_NO_UNMATCHEDBETS_VIEW = 5;
var controllers_RETRIEVING_MY_BETS_BET_VIEW = 6;

var controllers_STAKE_EXCEEDED_EXPOSURE = 'EXCEEDED_EXPOSURE_OR_AVAILABLE_TO_BET_BALANCE';
var controllers_STAKE_EXCEEDED_LOSS_LIMIT = 'LOSS_LIMIT_EXCEEDED';

var views_DEFAULT_SELECTED_COUPON_STAKE_BOX = "";

var controllers_MARKETVIEW_REFRESH_TIMEOUT = 30000;
var controllers_MARKETVIEW_REFRESH_POST_BET_TIMEOUT = 5000;

var controllers_MYBETS_REFRESH_TIMEOUT = 30000;
var controllers_MYBETS_REFRESH_POST_BET_TIMEOUT = 500;

var strToken = "[BETFAIR_TOKEN]";
var strToken2 = "[BETFAIR_TOKEN2]";
var strToken3 = "[BETFAIR_TOKEN3]";

var constants_UI_VIEW_COOKIE = "UIView";
var constants_BSP_LP_COOKIE = "bspLP";
var constants_BSP_MD_COOKIE = "bspMD";
var constants_UI_VIEW_EXCHANGE = "EXCHANGE";
var constants_UI_VIEW_COMPACT = "COMPACT";

var constants_USER_HISTORY_COOKIE = "userhistory";

var constants_SHOW_HELP_COOKIE = "ShowHelp";
var constants_AUTO_REFRESH_COOKIE = "AutoRefresh";
var constants_LAY_COLOUR_COOKIE = "LayColour";
var constants_SHOW_DEMO_HELP = "ShowDemoHelp";
var constants_OPEN_SUB_MENU = "OpenSubMenu";

// Exchange MiniGames cookies
var constants_MINI_GAMES_ENABLED_YN = "miniGamesEnabled";
var constants_DISPLAY_MINI_GAMES_YN = "miniGamesDisplayCarousel";
var constants_MINI_GAMES_MIN_MAX = "miniGamesCarouselMinMax";

var constants_MARKET_TYPE_ODDS = "Odds";
var constants_MARKET_TYPE_RANGE = "Range";
var constants_MARKET_TYPE_LINE = "Line";
var constants_MARKET_TYPE_ASIAN = "Asian";
var constants_MARKET_TYPE_ASIAN_COUPON = "Asian_Coupon";
var constants_MARKET_TYPE_COUPON_2RUNNER = "Coupon_2Runner";
var constants_MARKET_TYPE_COUPON_3RUNNER = "Coupon_3Runner";

function alertBetValues(frmName){
	if(allowDebugAlerts){
		var oForm = document.forms[frmName];
		var outputStr = "";
	//	outputStr += "Origin \t\t= "+document.forms[frmName].origin.value;
	//	outputStr += "\n";
		outputStr += "BetIDs \t\t= "+oForm.BetIDs.value;
		outputStr += "\n";
		outputStr += "SelectionIDs \t= "+oForm.SelectionIDs.value;
		outputStr += "\n";
		outputStr += "BidTypes \t\t= "+oForm.BidTypes.value;
		outputStr += "\n";
		outputStr += "Odds \t\t= "+oForm.Odds.value;
		outputStr += "\n";
		outputStr += "NewOdds \t= "+oForm.NewOdds.value;
		outputStr += "\n";
		outputStr += "Stakes \t\t= "+oForm.Stakes.value;
		outputStr += "\n";
		outputStr += "NewStakes \t= "+oForm.NewStakes.value;
		outputStr += "\n";
		outputStr += "Modes \t\t= "+oForm.Modes.value;
		outputStr += "\n";
		outputStr += "SubEventIds \t= "+oForm.SubEventIds.value;
		outputStr += "\n";
		outputStr += "BetType \t\t= "+oForm.BetType.value;
		outputStr += "\n";
		outputStr += "ei (EventID) \t= "+oForm.ei.value;
		outputStr += "\n";
		outputStr += "iid (InterfaceID) \t= "+oForm.iid.value;
		outputStr += "\n";
		outputStr += "fa \t\t= "+oForm.fa.value;
		outputStr += "\n";
		alert(outputStr);
	}
}

function setTabImageProperties(iTab, iTabCount){
	for (var i=1;i<iTabCount+1;i++) {
		document.getElementById("BMHeaderTab"+i).className = (iTab == i)? "BMHeaderTabActive" : "BMHeaderTabInActive";
	}
}

function batchProcessHide(){
	for (var i=0;i<arguments.length;i++) {
		if(arguments[i] == "BMMinigamesWrapper") {
			document.getElementById(arguments[i]).style.left = "-100000px";
			document.getElementById(arguments[i]).style.right = "-100000px";			
		} else {
			document.getElementById(arguments[i]).style.display = "none";
		}
	}
}

function setButtonProperties(backValue, layValue){
	document.getElementById("BMPlaceBetsBetViewHeaderBackAllButton").value = backValue;
	document.getElementById("BMPlaceBetsDefaultViewHeaderBackAllButton").value = backValue;
	document.getElementById("BMPlaceBetsBetViewHeaderLayAllButton").value = layValue;
	document.getElementById("BMPlaceBetsDefaultViewHeaderLayAllButton").value = layValue;
}

function formatRunnerHC(iAmount){
//	iAmount = DoRnd(iAmount, 2)
//	var wholeNumber = Math.floor(iAmount)
	var abolsuteAmmount = Math.abs(iAmount);
	var topNumber = Math.ceil(abolsuteAmmount);
	var bottomNumber = Math.floor(abolsuteAmmount);
	var newAmount;
	var sign = (iAmount > 0)? "+" : "-";
	
	if(abolsuteAmmount == bottomNumber){
		newAmount = sign+roundAsianHC(abolsuteAmmount);
	}else if(abolsuteAmmount == bottomNumber + 0.5){
		newAmount = sign+roundAsianHC(abolsuteAmmount);
	}else if(abolsuteAmmount - 0.25 > bottomNumber + 0.5){
		newAmount = sign+roundAsianHC(abolsuteAmmount - 0.25)+"&"+ sign+roundAsianHC(abolsuteAmmount + 0.25);
	}else{
		newAmount = sign+roundAsianHC(abolsuteAmmount - 0.25)+"&"+ sign+roundAsianHC(abolsuteAmmount + 0.25);
	}
	return " "+newAmount;

	/*
	if(Math.abs(iAmount) - 0.25 >= Math.abs(wholeNumber)+0.5){
		newAmount = roundAsianHC(Math.abs(wholeNumber)+(Math.abs(iAmount) - 0.25))+" & "+ roundAsianHC(Math.abs(iAmount) + 0.25)
	}else if(Math.abs(iAmount) - 0.25 >= Math.abs(wholeNumber)+0.25 || Math.abs(iAmount) - 0.25 >= Math.abs(wholeNumber)+0.75){
		newAmount = roundAsianHC(Math.abs(wholeNumber)+(Math.abs(iAmount) - 0.25))+" & "+ roundAsianHC(Math.abs(iAmount) + 0.25)
	}else{
		newAmount = roundAsianHC(iAmount)
	}
	return " "+newAmount;
	*/
}

function roundAsianHC(iAmount){
	var newAmount = (iAmount == 0)? 0 : (iAmount==Math.floor(iAmount))?iAmount+'.0':((iAmount*10==Math.floor(iAmount*10))?iAmount+'':iAmount);
	return newAmount;
}

function calcThreeQuart(stake, odds) {
	return (stake / 2) + ((stake / 2) * odds);
}

//var strToken3 = "-betFairToken-"

function getSubStr(str1, str2, sTokenID){
	var s = str1.lastIndexOf(sTokenID);
	if(s != -1){
		var sPart1 = str1.substring(0, s);
		var sPart2 = str1.substring(s + sTokenID.length, str1.length);
		return(sPart1 + str2 + sPart2);
	}else{
		return false;
	}
}

function compareObjects(newObject, oldObject) {
    for (iCountB in newObject) {
        if (typeof newObject[iCountB] != 'object') {
			if (newObject[iCountB] != oldObject[iCountB]) {
				return true;
			}
        }
    }
	return false;
}

function cloneObject(what, bNoSubObjects) {
    for (i in what) {
        if (typeof what[i] == 'object' && what[i] != null && !bNoSubObjects) {
            this[i] = new cloneObject(what[i]);
        } else {
            this[i] = what[i];
 		}
    }
}

try{
	interface_registerJSResource("Helper");
}catch(x){
	
}
