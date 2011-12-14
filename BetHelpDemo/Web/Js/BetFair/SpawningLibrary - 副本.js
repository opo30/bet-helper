/*
TODO: JPE
	this array should be an associative array rather than an regular array
	this will stop having to increment and re-jig the numbers each time we add a new popup
	all code referencing the spawns need not be maintained if we use names rather than ids
	closeSpawn will then not need a case statement, we can just use the name passed in
	we can also refactor the queryStringInfo into a utility function
ie	
	
	oSpawnCollection["joinNow"]
	getWindow(oSpawnCollection, "joinNow", "/account/registration/BeginRegistration.do",0,535,700);
	
	reference by : oSpawnCollection[18]
	getWindow(oSpawnCollection, 18, "/account/registration/BeginRegistration.do",0,535,700);	
*/

oSpawnCollection = new Array();
var pageOfOrigin; //allows for tracking tags when spawning demo

function spawn(sWhichSpawn) {
	var returnVal = null;
	
	switch (sWhichSpawn) {
		case "aboutUs":
			var queryStringInfo = "aboutus/?product=exchange" +
				"&region=" + GetCookie("betexRegionSessionCookie", "") +
				"&locale=" + GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" + GetCookie("betexBrand", "");

			getWindow(oSpawnCollection, 0, content_URL+queryStringInfo,0,500,788);
		break;
		case "betfairCharges":
			var url = "aboutus/?product=exchange";
				if(GetCookie("betexRegionSessionCookie", "")) {
					url = url +	"&region=" + GetCookie("betexRegionSessionCookie", "");
				}
				if(GetCookie("betexLocaleSessionCookie", "")) {
					url = url +	"&locale=" + GetCookie("betexLocaleSessionCookie", "");
				}
				if(GetCookie("betexBrand", "")) {
					url = url +	"&brand=" + GetCookie("betexBrand", "");
				}
				url += "&sWhichKey=Betfair Charges&sWhichAnchor=DataRequestCharges";
			getWindow(oSpawnCollection, 1, content_URL+url, 0, 500, 788);
		break;
		case "aboutUsTermsAndConditions":
			var url = "aboutus/?product=exchange";
				if(GetCookie("betexRegionSessionCookie", "")) {
					url = url +	"&region=" + GetCookie("betexRegionSessionCookie", "");
				}
				if(GetCookie("betexLocaleSessionCookie", "")) {
					url = url +	"&locale=" + GetCookie("betexLocaleSessionCookie", "");
				}
				if(GetCookie("betexBrand", "")) {
					url = url +	"&brand=" + GetCookie("betexBrand", "");
				}
				if (arguments[1] && arguments[1] != '') {
					url = url + "&jumpTo=" + arguments[1];
				}
				url += "&sWhichKey=Terms and Conditions";
			getWindow(oSpawnCollection, 1, content_URL+url, 0, 500, 788);
		break;
		case "affiliateScheme":
			getWindow(oSpawnCollection, 2, "http://www.betfairpromo.com/demo2/"+arguments[1]+"/index.html",0,500,788);
		break;
		case "betBreakdown":
			var queryStringInfo = "?betId=" + arguments[1];
			if(arguments[2] != null) {
				queryStringInfo = queryStringInfo + 
					"&subAccountID=" + arguments[2];
			}
			getWindow(oSpawnCollection, 3, "/reporting/bethistory/LoadBetBreakdownAction.do" + queryStringInfo,0,300,620);
		break;
		case "betfairPoker":
			getWindow(oSpawnCollection, 4, "/pokerroom/Index.do?download=true",0,500,800);
		break;
		case "betfairShop":
			getWindow(oSpawnCollection, 5, "http://www.betfairshop.com",0,500,788);
		break;
		case "cardCountries":
			var dMgr = getDomainManager();
			getWindow(oSpawnCollection, 6, dMgr.constructUrlWithPath("myaccount","cardCountries",true),0,420,270);
		break;
		case "cardPolicy":
			var dMgr = getDomainManager();
			getWindow(oSpawnCollection, 7, dMgr.constructUrlWithPath("myaccount","cardPolicy",true),0,600,410);
		break;
		case "commission":
			var queryStringInfo = "help/?product=exchange" +
				"&region=" + GetCookie("betexRegionSessionCookie", "") +
				"&locale=" + GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" + GetCookie("betexBrand", "") + 
				"&sWhichKey=Help.Managing.Account&sWhichAnchor=CommissionPoints";
			getWindow(oSpawnCollection, 18, content_URL+queryStringInfo,0,500,788);			
		break;
		case "contactUs":
			var queryStringInfo = 
				"?region=" +
				GetCookie("betexRegionSessionCookie", "") +
				"&locale=" +
				GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" +
				GetCookie("betexBrand", "");
			getWindow(oSpawnCollection, 9, contactUs_URL+queryStringInfo,0,500,788);
		break;		
		case "customiseMenu":
			return getWindow(oSpawnCollection, 10, "/menu/customise/Index.do?locale=" + GetCookie("betexLocaleSessionCookie", ""),0,600,500);
		break;
		case "cvv2":
			var dMgr = getDomainManager();
			getWindow(oSpawnCollection, 11, dMgr.constructUrlWithPath("myaccount","cvv2",true),0,380,470);
		break;
		case "deepMarketLink":
			var dMgr = getDomainManager();
			var url = "http://" + dMgr.getTopDomain("sports") + arguments[1];
			getWindow(oSpawnCollection, 12, url, 0, 500, 788);
		break;
		case "ecoCard":
			getWindow(oSpawnCollection, 13, "http://www.ecocard.com",0,600,410);
		break;
		case "exchangePokerGameHistory":
			getWindow(oSpawnCollection, 14, "/exchangepoker/LoadExchangePokerHistoryAction.do"+arguments[1]+"",0,500,788);
		break;
		case "forgottenPassword":
			var url = "/account/forgotpassword/ForgottenPassword.do?method=initialise";
			if (arguments[1] && arguments[1] != '') {
				url = url + "&" + arguments[1] + "=" + arguments[2] + "&" + new Date().getTime();
			} 
			getWindow(oSpawnCollection, 15, url, 0, 500, 788);
		break;
		case "forum":
			var queryStringInfo = 
				"?timeZoneID=" + 
				GetCookie("betexTimeZoneSessionCookie", "");
			getWindow(oSpawnCollection, 16, "/account/ForumRedirect.do"+queryStringInfo,0,500,788,true);
		break;
		case "gamCare":
			var queryStringInfo = 
				"&region=" +
				GetCookie("betexRegionSessionCookie", "") +
				"&locale=" +
				GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" +
				GetCookie("betexBrand", "");

			var url = content_URL + "misc/?product=portal&sWhichKey=GamCare" + queryStringInfo;
			getWindow(oSpawnCollection, 17, url,0,500,640);
		break;
		case "help":
			if ((arguments[1] == 'Help.Contact.Us') && !arguments[2]) {
				spawn("cmsContactUs");
			} else {
				var locale = GetCookie("betexLocaleSessionCookie", "");
				var region = GetCookie("betexRegionSessionCookie", "");
				var queryStringInfo = "help/" +
					"?product=exchange" +
					"&region=" + region +
					"&locale=" + locale +
					"&brand=" + GetCookie("betexBrand", "") + 
					"&sWhichKey=" + arguments[1] +
					"&sWhichAnchor=" + arguments[2];

				getWindow(oSpawnCollection, 18, content_URL+queryStringInfo,0,500,788);			
			}
		break;
		case "joinNow":
			// Belinda 2005-07-28 defect 17331 - now uses https
			var url = joinNow_URL;

			var queryStringInfo = "";
			if(document.GetCookie && GetCookie("betexRegionSessionCookie", "")) {
				queryStringInfo = queryStringInfo + "?pi.RegionId=" + GetCookie("betexRegionSessionCookie", "");
			}
			if(document.GetCookie && GetCookie("betexLocaleSessionCookie", "")) {
				queryStringInfo = queryStringInfo + "&pi.localeId=" + GetCookie("betexLocaleSessionCookie", "");
			}
			if(document.GetCookie && GetCookie("betexBrand", "")) {
				queryStringInfo = queryStringInfo + "&pi.brandId=" + GetCookie("betexBrand", "");
			}

			url += queryStringInfo;
			
			if (arguments[1] && arguments[1] != '') {
				if(queryStringInfo && queryStringInfo != '') {
					url = url + "&";
				}
				else {
					url = url + "?";
				}
				
				url = url + arguments[1] + "=" + arguments[2];
			} 
			getWindow(oSpawnCollection, 19, url,0,535,700,true);
		break;
		case "marketUnavailable":
			getWindow(oSpawnCollection, 20, "/MarketUnavailable.do",0,400,600);
		break;
		case "moreInfoChart":
			var queryStringInfo = 
				"&timeZone=" +
				GetCookie("betexTimeZoneSessionCookie", "") +
				"&region=" +
				GetCookie("betexRegionSessionCookie", "") +
				"&locale=" +
				GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" +
				GetCookie("betexBrand", "") + 
				"&currency=" +
				GetCookie("betexCurrencySessionCookie", "");	
			var baseURL = ((arguments[2]) ? arguments[2] : "");
			getWindow(oSpawnCollection, 21, baseURL + "betting/LoadRunnerInfoAction.do"+arguments[1]+ queryStringInfo + "",0,500,700);
		break;
		case "myAccount":			
			return getWindow(oSpawnCollection, 22, ""+arguments[1]+""+arguments[2]+""+arguments[3]+"",1,550,788);
		break;
		case "myAccountFromRegistration":
			getWindow(oSpawnCollection, 23, ""+arguments[1]+""+arguments[2]+"",1,500,788);
		break;
		case "newTAndCs":
			getWindow(oSpawnCollection, 24, "/account/termsandconditions/NotifyOfChanges.do",0,520,650);
		break;
		case "oddsConverter":
			var queryStringInfo = 
				"?timeZone=" +
				GetCookie("betexTimeZoneSessionCookie", "") +
				"&region=" +
				GetCookie("betexRegionSessionCookie", "") +
				"&locale=" +
				GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" +
				GetCookie("betexBrand", "");		
			getWindow(oSpawnCollection, 25, "/betting/OddsConversion.do" + queryStringInfo,0,640,350);
		break;
		case "oddsEquivalent":
			getWindow(oSpawnCollection, 26, ""+arguments[1]+"",0,500,700);
		break;
		case "onePay":
			getWindow(oSpawnCollection, 27, "http://www.drhobetspromotion.com/"+arguments[1]+"",0,440,601);
		break;
		case "openNews":
			getWindow(oSpawnCollection, 28, "/News.do?ni=1",0,640,650);
		break;
		case "payPalCountries":
			getWindow(oSpawnCollection, 29, ""+arguments[1]+"/payment/paypal/PayPalCountries.do",0,420,270);
		break;
		case "pokerAboutUs":
			getWindow(oSpawnCollection, 30, "/pokerroom/aboutus/Index.do",0,500,788);
		break;
		case "privacyPolicy":
				var url = 'aboutus/?product=exchange';
				if(GetCookie("betexRegionSessionCookie", "")) {
					url = url +	"&region=" + GetCookie("betexRegionSessionCookie", "");
				}
				if(GetCookie("betexLocaleSessionCookie", "")) {
					url = url +	"&locale=" + GetCookie("betexLocaleSessionCookie", "");
				}
				if(GetCookie("betexBrand", "")) {
					url = url +	"&brand=" + GetCookie("betexBrand", "");
				}
				if (arguments[1] && arguments[1] != '') {
					url = url + "&jumpTo=" + arguments[1];
				}

				if (arguments[2] && arguments[3] && arguments[2] != '' && arguments[3] != '') {
					url = url + "&" + arguments[2] + "=" + arguments[3];
				}
				url = url + "&sWhichKey=Privacy Policy";
				getWindow(oSpawnCollection, 31, content_URL+url, 0, 500, 788);
		break;
		case "referAFriend":
			getWindow(oSpawnCollection, 32, "/communications/ReferAFriend.do",0,640,450);
		break;
		case "referAFriendTermsAndConditions":
			getWindow(oSpawnCollection, 33, "/communications/ReferAFriendTermsAndConditions.do",0,640,450);
		break;
		case "trustWise":
			getWindow(oSpawnCollection, 34, "/account/registration/TrustwiseSiteSeal.do",0,540,650);
		break;
		case "whereIsTheMoney":			
			var queryStringInfo = 
				"&timeZone=" +
				GetCookie("betexTimeZoneSessionCookie", "") +
				"&region=" +
				GetCookie("betexRegionSessionCookie", "") +
				"&locale=" +
				GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" +
				GetCookie("betexBrand", "") + 
				"&currency=" +
				GetCookie("betexCurrencySessionCookie", "");
			getWindow(oSpawnCollection, 35, "/betting/WheresTheMoneyAction.do?sortId=2" + queryStringInfo,0,700,670);
		break;
		case "jobs":
			getWindow(oSpawnCollection, 36, "http://www.betfair.com/jobs/jobs.asp",0,700,670);
		break;
		case "kycBetfair":
			getWindow(oSpawnCollection, 37, "http://kyc.betfair.com",false,500,788);
		break;

		case "kycBetfairAus":
			getWindow(oSpawnCollection, 37, "http://kyc.betfair.com/ausmarkets.html",false,500,788);
		break;		
		case "kycBetfairAusCustomer":
			getWindow(oSpawnCollection, 37, "http://kyc.betfair.com/auscustomers.html",false,500,788);
		break;
		case "rulesAndRegulations":
			var queryStringInfo = 
				"?region=" +
				GetCookie("betexRegionSessionCookie", "") +
				"&locale=" +
				GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" +
				GetCookie("betexBrand", "");
				getWindow(oSpawnCollection, 38, rulesAndRegulations_URL+queryStringInfo,0,500,788);
		break;
  		case "12plays":
			getWindow(oSpawnCollection, 40, "http://www.betfairpromo.com/12plays",0,500,788);  		
  		break;
		case "responsibleGambling":
			var queryStringInfo = 
				"&region=" +
				GetCookie("betexRegionSessionCookie", "") +
				"&locale=" +
				GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" +
				GetCookie("betexBrand", "");

			var url = content_URL + "misc/?product=portal&sWhichKey=ResponsibleGambling" + queryStringInfo;
			getWindow(oSpawnCollection, 41, url,0,650,640);
		break;
 		case "betfairGuide":
			var url = "http://www.betfairpromo.com/demo/index.asp?locale=" + GetCookie("betexLocaleSessionCookie", "");
			if (arguments[1] && arguments[2] && arguments[1] != '' && arguments[2] != '') {
				url = url + "&" + arguments[1] + "=" + arguments[2];
			}
			
			getWindow(oSpawnCollection, 42, url, false, 465, 730);
 		break;
		case "faqs":
				var url = 'faqs/?product=exchange'; 
				if(GetCookie("betexRegionSessionCookie", "")) {
					url = url +	"&region=" + GetCookie("betexRegionSessionCookie", "");
				}
				if(GetCookie("betexLocaleSessionCookie", "")) {
					url = url +	"&locale=" + GetCookie("betexLocaleSessionCookie", "");
				}
				if(GetCookie("betexBrand", "")) {
					url = url +	"&brand=" + GetCookie("betexBrand", "");
				}
				getWindow(oSpawnCollection, 43, content_URL+url, 0, 500, 788);
		break;		
		case "trustDeed":
			var url = "aboutus/?product=exchange";
				if(GetCookie("betexRegionSessionCookie", "")) {
					url = url +	"&region=" + GetCookie("betexRegionSessionCookie", "");
				}
				if(GetCookie("betexLocaleSessionCookie", "")) {
					url = url +	"&locale=" + GetCookie("betexLocaleSessionCookie", "");
				}
				if(GetCookie("betexBrand", "")) {
					url = url +	"&brand=" + GetCookie("betexBrand", "");
				}
				url += "&sWhichKey=Trust Deed";
			getWindow(oSpawnCollection, 44, content_URL+url, 0, 500, 788);
		break;			
		case "aboutUsRacingWelfare":
				//this can only be spawned is we are using the external CMS
				var url = arguments[1] + "aboutus/" + "?tabId=racingWelfare";
				
				if(GetCookie("betexRegionSessionCookie", "")) {
					url = url + "&region=" + GetCookie("betexRegionSessionCookie", "");
				}
				if(GetCookie("betexLocaleSessionCookie", "")) {
					url = url + "&locale=" + GetCookie("betexLocaleSessionCookie", "");
				}
				if(GetCookie("betexBrand", "")) {
					url = url + "&brand=" + GetCookie("betexBrand", "");
				}
			getWindow(oSpawnCollection, 45, url, 0, 500, 788);
		break;
		case "exchangeWebHomePage":
			getWindow(oSpawnCollection, 46, "http://www.betfair.com",0,600,800,true,true,true);
		break;
		case "netellerWithdrawalHelp":
			getWindow(oSpawnCollection, 47, "/myaccount/payment/netellerWithdrawalHelp.do",0,280,470);
		break;
		case "priceComparison":
			var url = "/PriceComparison.do";
			if (arguments[1] && arguments[2] && arguments[1] != '' && arguments[2] != '') {
				url = url + "?" + arguments[1] + "=" + arguments[2];
			}
			getWindow(oSpawnCollection, 48, url,0,600,850);
		break;
		case "ageRestriction":
			getWindow(oSpawnCollection, 49, "/myaccount/account/registration/AgeRestriction.do",0,250,300);
		break;
		case "payments":
			var url = "http://payments.betfair.com";
			if (arguments[1] && arguments[2] && arguments[1] != '' && arguments[2] != '') {
				url = url + "?" + arguments[1] + "=" + arguments[2];
			}
			if (arguments.length > 3) {
				getWindow(oSpawnCollection, 50, arguments[3],0,400,800);
			} else {
				var region = GetCookie("betexRegionSessionCookie", "");
				getWindow(oSpawnCollection, 50, url,0,400,800);			
			}
		break;		
		case "betting":
			getWindow(oSpawnCollection, 51, "http://betting.betfair.com",0,400,800);
		break;		
		case "cmsContactUs":		
			var queryStringInfo = 
				"aboutus/?product=exchange" +
				"&region=" + GetCookie("betexRegionSessionCookie", "") +
				"&locale=" + GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" + GetCookie("betexBrand", "") +
				"&sWhichKey=Contact Us";
			getWindow(oSpawnCollection, 52, content_URL+queryStringInfo,0,500,788);
		break;		
 		case "tournamentGuide":
			var locale = GetCookie("betexLocaleSessionCookie", "");
			var url = "";

			if (locale.substring(0,2) == "de") {
				FILogger("WCD_DE", false);
				url = "http://www.betfairpromo.com/worldcup2006demo/de";
			} else {
				FILogger("WCD_EN", false);
				url = "http://www.betfairpromo.com/worldcup2006demo";
			}
			getWindow(oSpawnCollection, 53, url,0,460,720);
 		break;
 		case "tgc":
			var queryStringInfo = 
				"&region=" +
				GetCookie("betexRegionSessionCookie", "") +
				"&locale=" +
				GetCookie("betexLocaleSessionCookie", "") +
				"&brand=" +
				GetCookie("betexBrand", "");

			var url = content_URL + "misc/?product=exchange&sWhichKey=TGC" + queryStringInfo;
		
			getWindow(oSpawnCollection, 54, url, 0, 400, 440);
 		break;
 		case "HR":
			getWindow(oSpawnCollection, 55, "http://racing.betfair.com?origin=" + arguments[1],0,600,845);
 		break;
 		case "walletTransfer":
			getWindow(oSpawnCollection, 56, ""+arguments[1]+"?transfer=1",1,420,520);
 		break;
 		case "betfairOverview":
			getWindow(oSpawnCollection, 57, "http://www.betfairpromo.com/demo/index.asp?locale=" + arguments[1],true,460,712);
 		break;
 		case "placingABet":
			switch(arguments[1]) {
				case "bg":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=bg",0,460,727);
				break;
				case "cz":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=cz",0,460,727);
				break;
				case "da":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=da",0,460,727);
				break;
				case "de":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=de",0,460,727);
				break;
				case "el":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=el",0,460,727);
				break;
				case "en":
					getWindow(oSpawnCollection, 58, "http://www.betfairpromo.com/learnbetfair",0,581,650);
				break;
				case "es":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=es",0,460,727);
				break;
				case "fi":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=fi",0,460,727);
				break;
				case "it":
					getWindow(oSpawnCollection, 58, "http://www3.betfairpromo.com/demo/index.asp?locale=it",0,460,727);
				break;
				case "no":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=no",0,460,727);
				break;
				case "pl":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=pl",0,460,727);
				break;
				case "pt":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=pt",0,460,727);
				break;
				case "ru":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=ru",0,460,727);
				break;
				case "sv":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=se",0,460,727);
				break;
				case "tr":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=tr",0,460,727);
				break;
				case "zh":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=zh",0,460,727);
				break;
				case "zh_TW":
					getWindow(oSpawnCollection, 58, "http://promo.betfair.com/demo/index.asp?locale=cn",0,460,727);
				break;
				default:
					getWindow(oSpawnCollection, 58, "http://www.betfairpromo.com/learnbetfair",0,581,650);
				break;
			}
 		break;
 		case "bettingGuide":
			var locale = GetCookie("betexLocaleSessionCookie", "");
			var region = GetCookie("betexRegionSessionCookie", "");
			getWindow(oSpawnCollection, 59, content_URL + "downloads/" + ((region == "ASIA") ? region : locale ) + "/Betfair_Guide_to_Betting.pdf",0,600,800);
 		break;
		case "newHelp":
			getWindow(oSpawnCollection, 60, ""+arguments[1],0,500,788);
		break;
		case "weeklySportsDiary":
			var locale = GetCookie("betexLocaleSessionCookie", "");
			var region = GetCookie("betexRegionSessionCookie", "");
			getWindow(oSpawnCollection, 61, "http://tvguide.betfair.com/?locale=" + locale + "&region=" + region,0,600,810, true);
  		break;
	}
}

function closeSpawn(sWhichSpawn) {

	switch(sWhichSpawn) {
		case "joinNow":
			try {
				oSpawnCollection[19].close();
				oSpawnCollection[19] = null;
			}
			catch(x) {

			}
		break;		
		case "myAccount":
			try {
				oSpawnCollection[22].close();
				oSpawnCollection[22] = null;
			} 
			catch(x) {

			}
		break;
		case "myAccountFromRegistration":
			try {
				oSpawnCollection[23].close();
				oSpawnCollection[23] = null;
			}
			catch(x) {

			}
		break;
	}

}

function getWindow(oWindowArray, iWindowArrayIndex, sURL, bAutoScroll, iHeight, iWidth, bStatusBar, bMenuBar, bToolBar) {
	try {		
		oWindowArray[iWindowArrayIndex].document.location = sURL;
		oWindowArray[iWindowArrayIndex].focus();
		return oWindowArray[iWindowArrayIndex];
	}
	catch(x) {
		try {			
			//For 'myAccount'; re-setting domain name to main domain if request is coming from multiples sub-domain
			if(iWindowArrayIndex == '22' && checkForMulitples(parent.frames['main'].document.location.href)){
				/**if(sMultiplesDomain != null && sMultiplesDomain != ""){							
					var indx1 = sMultiplesDomain.indexOf('//');
					var Str1 = sMultiplesDomain.substring(0, indx1+2);
					var Str2 = sMultiplesDomain.substring(indx1+2,sMultiplesDomain.lastIndexOf('/'));
					var indx2 = Str2.indexOf('.');
					var Str3 = Str2.substring(indx2+1);				
					sURL = Str1 + Str3 + sURL;		
				}*/
				if(sSinglesDomain != null && sSinglesDomain != ""){
					var domainLength = 	sSinglesDomain.length;					
					if(sSinglesDomain.charAt(domainLength - 1) == '/'){
						sSinglesDomain = sSinglesDomain.substring(0, domainLength - 1);						
						sURL = 	sSinglesDomain + sURL;							
					}	else {
						sURL = 	sSinglesDomain + sURL;					
					}	
				}
			}			
			oWindowArray[iWindowArrayIndex] = window.open(sURL, "betfairSpawn"+iWindowArrayIndex,'height='+ iHeight +',width='+ iWidth +',top=' + ((screen.availHeight - iHeight) / 2) + ",screenY=" + ((screen.availHeight - iHeight) / 2) + ",left=" + ((screen.availWidth - iWidth) / 2) + ",screenX=" + ((screen.availWidth - iWidth) / 2) +",scrollbars="+((bAutoScroll)? "no":"yes")+",status="+((bStatusBar)? "yes":"no")+",resizable=yes,menubar="+((bMenuBar)? "yes":"no")+",toolbar="+((bToolBar)? "yes":"no")+",titlebar=yes')");
			return oWindowArray[iWindowArrayIndex];
		}
		catch(x) {
			if (loginMsg_SpawnBlocker != "") {
				alert(loginMsg_SpawnBlocker);
				return null;
			}
		}
	}
}

function checkPopUpWithIdExists(id) {
     if(isNaN(parseFloat(id))) { 
     	return false;
     }
 
     for(i = 0; i < oSpawnCollection.length; i++) {
     	if (oSpawnCollection[i] && !oSpawnCollection[i].closed) {
     		if (i == id) {
     			return true;
     		}
     	}
     }
     return false;
}


function getDomainManager() {
	var dManager = null;
	
	if (typeof (domainManager) != "undefined") {
		dManager = domainManager;
	} else if (typeof (parent.domainManager) != "undefined") {
		dManager = parent.domainManager;
	} else if (typeof (top.domainManager) != "undefined") {
		dManager = top.domainManager;
	}
	
	return dManager;
}

try {
	interface_registerJSResource("spawnInterface");
}
catch(x) {
	
}
