function interfaces_getNewUser(locale, region, timezone, currency, couponStake1, couponStake2, couponStake3, couponStake4, 
	couponStake5, displayProfitAndLoss, includeSettledBets, includeNetCommision, displayFutureProfitAndLoss, displayStakeLadder, 
	couponOddsDisplay, autoRefresh, layColour, liabilityView, showHelp, verifyBet, percentageBook, betInfo, matchedBets, 
	consolidated, averageOdds, uiView, enhancedView, loggedIn, name, accountID, accountBalance, isAllowedToBetInPlay, 
	commissionPrice, defaultExchange, showDemoHelp, showFractionalOdds, flashUpdate, tournamentNav, 
	displayLayPrices, displayMarketDepth, displayStartingPrices, displayProjectedOdds, showMouseOverHelp,
	showBetSummaryMessaging, displayBetHelpTabDefault, displayMyMarketsDefault, displayRaceCardInfo, showRaceDetailsMouseOvers, 
	clusterUnmatchedBets, preferenceGroupTitleStates, displayBetStatusTitle, showBspWarning, useFloatingBetSlip, isInternationalCustomer, 
	isInternationalTermsAccepted, displayZoneVideo, displayLiveScores, multiplesUIView, displayMiniGames, miniGamesEnabled, miniGamesMinimizedState,
	marketViewDisplayType, customerNumber) {
	
	var newUser = new models_User();

	newUser.locale = (locale)? locale : null;
	newUser.region = (region)? region : null;
	newUser.timezone = (timezone)? timezone : null;
	newUser.currency = (currency)? currency : null;

	var oPrefs = newUser.preferences;
	oPrefs.couponStake1 = (couponStake1)? couponStake1 : null;
	oPrefs.couponStake2 = (couponStake2)? couponStake2 : null;
	oPrefs.couponStake3 = (couponStake3)? couponStake3 : null;
	oPrefs.couponStake4 = (couponStake4)? couponStake4 : null;
	oPrefs.couponStake5 = (couponStake5)? couponStake5 : null;

	oPrefs.displayProfitAndLoss = displayProfitAndLoss;
	oPrefs.includeSettledBets = includeSettledBets;
	oPrefs.includeNetCommision = includeNetCommision;
	oPrefs.displayFutureProfitAndLoss = displayFutureProfitAndLoss;
	oPrefs.displayStakeLadder = displayStakeLadder;
	oPrefs.couponOddsDisplay = couponOddsDisplay;
	oPrefs.autoRefresh = autoRefresh;
	oPrefs.layColour = layColour;
	oPrefs.liabilityView = liabilityView;
	oPrefs.showHelp = showHelp;
	oPrefs.enhancedView = displayRaceCardInfo; //enhancedView;
	//Must default to true on errors.
	if (verifyBet === false) {
		oPrefs.verifyBet = false;
	}
	else {
		oPrefs.verifyBet = true;
	}
	
	oPrefs.percentageBook = percentageBook;
	oPrefs.betInfo = betInfo;
	oPrefs.matchedBets = matchedBets;
	oPrefs.consolidated = consolidated;
	oPrefs.averageOdds = averageOdds;
	oPrefs.uiView = uiView;
	oPrefs.showDemoHelp = showDemoHelp;
	oPrefs.showFractionalOdds = showFractionalOdds;
	oPrefs.flashUpdate = flashUpdate;
	oPrefs.tournamentNavigation = tournamentNav;
	
	oPrefs.displayLayPrices = displayLayPrices;
	oPrefs.displayMarketDepth = displayMarketDepth;
	oPrefs.displayStartingPrices = displayStartingPrices;
	oPrefs.displayProjectedOdds = displayProjectedOdds;
	oPrefs.showMouseOverHelp = showMouseOverHelp;
	oPrefs.showBetSummaryMessaging = showBetSummaryMessaging;
	oPrefs.displayBetHelpTabDefault = displayBetHelpTabDefault;
	oPrefs.displayMyMarketsDefault = displayMyMarketsDefault;
	oPrefs.displayRaceCardInfo = displayRaceCardInfo;
	oPrefs.showRaceDetailsMouseOvers = showRaceDetailsMouseOvers;
	oPrefs.clusterUnmatchedBets = clusterUnmatchedBets;
	oPrefs.preferenceGroupTitleStates = preferenceGroupTitleStates;
	oPrefs.displayBetStatusTitle = displayBetStatusTitle;
	oPrefs.showBspWarning = showBspWarning;
	oPrefs.useFloatingBetSlip = useFloatingBetSlip;

	newUser.isInternationalCustomer = isInternationalCustomer;
	newUser.isInternationalTermsAccepted = isInternationalTermsAccepted;
	
	oPrefs.displayZoneVideo = displayZoneVideo;
	oPrefs.displayLiveScores = displayLiveScores;
	oPrefs.multiplesUIView = multiplesUIView;
	// Minigames Prefs
	oPrefs.displayMiniGames = displayMiniGames;
	oPrefs.miniGamesEnabled = miniGamesEnabled;
	oPrefs.miniGamesMinimizedState = miniGamesMinimizedState;
	
	newUser.loggedIn = (loggedIn)? loggedIn : false;
	newUser.name = (name)? name : null;
	newUser.accountID = (accountID)? accountID : null;	
	newUser.accountBalance = (accountBalance)? accountBalance : null;
	newUser.isAllowedToBetInPlay = (isAllowedToBetInPlay)? isAllowedToBetInPlay : null;
	newUser.commissionPrice = commissionPrice;
	if (defaultExchange!=undefined && defaultExchange!=null) {
	   newUser.defaultExchange = defaultExchange;
	}
	oPrefs.marketViewDisplayType = marketViewDisplayType;
	newUser.customerNumber = customerNumber;
	return newUser;
}

function interfaces_getUserState(user){
	if (user) {
		return user.loggedIn;		
	}
	return null;

}

function interfaces_getUserName(user){
	return user.name;
}

function interfaces_getCustomerNumber(user) {
	return user.customerNumber;
}

function interfaces_getUserAccountID(user){
	return user.accountID;
}

function interfaces_getUserAccountBalance(user){
	return user.accountBalance;
}

function interfaces_getUserLocale(user){
	return user.locale;
}

function interfaces_getUserRegion(user){
	return user.region;
}

function interfaces_getUserTimeZone(user){
	return user.timezone;
}

function interfaces_getUserCurrency(user){
	return user.currency;
}

function interfaces_getUserPreferences(user){
	return user.preferences;
}

function interfaces_getUserDefaultExchange(user) {
    return user.defaultExchange;
}

try{
	interface_registerJSResource("userModelInterface");
}catch(x){
	
}
