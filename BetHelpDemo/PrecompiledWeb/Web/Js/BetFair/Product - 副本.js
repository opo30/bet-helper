/**
Functions Added for Multiples:
This function returns 'true' if the loaded frame has Multiples Contents 
*/
function checkForMultiples(mainFrameHref){	
	var isMultiplesPage = false;
	//alert("Utilities.js: checkForMultiples("+mainFrameHref+")");
	if(mainFrameHref != null){			
		urlContentArray = mainFrameHref.substring(0,mainFrameHref.length-1).split("/");		
				for (var i = 0; i < urlContentArray.length; i++) {									
					if(urlContentArray[i] == 'multiples'){					
						isMultiplesPage = true;
						break;
					}	
					else {
						isMultiplesPage = false;
					}				
				}	
	}
	return isMultiplesPage;
}