
function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_showHideLayers() { //v6.0
  var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }
    obj.visibility=v; }
}

function getCookie(name)
{
	var cname = name + "=";
	var dc = document.cookie;
	if (dc.length > 0) 
	{
		begin = dc.indexOf(cname);
		if (begin != -1) 
		{
			begin += cname.length;
			end = dc.indexOf(";", begin);
			if (end == -1) end = dc.length;
			return dc.substring(begin, end);
		}
	}
	return null;
}
function writeCookie(name, value) 
{ 
	var expire = ""; 
	var hours = 365;
	expire = new Date((new Date()).getTime() + hours * 3600000); 
	expire = ";path=/;expires=" + expire.toGMTString(); 
	document.cookie = name + "=" + value + expire; 
}

//显示进球窗口
var startani_C,startani_A,startani_B,pop_TC;
var oPopup;
try{ oPopup=window.createPopup();}
catch(e){}

function ShowCHWindow(str,matchnum)
{
imagewidth=460;
imageheight=28+33*matchnum ;

var st="<table width=460 border=0 cellpadding=0 cellspacing=0 style='border: 3px solid #090;background-color: #FFF;'>";
st=st + "<tr style='background-color: #DBECA6;'><td height=22 colspan=6><SPAN style='margin-left:6px'><B>SeoBet 入球提示</B></SPAN></td></tr>";
st=st + str;
st=st + "</table>";  
st=st + "<style type='text/css'>";
st=st + "td {font-family: 'Tahoma', '宋体';font-size: 13px;}";
st=st + ".line td { border-bottom:solid 1px #FFD8CA; line-height:32px;}";
st=st + "</style>";

x=280;
y=1;
switch(Config.winLocation)
{			
    case 0:
        x=(screen.width-imagewidth)/2;
        y=1;
        break;
    case 1:
        x=(screen.width-imagewidth)/2;
        y=screen.height-imageheight-30;
        break;
    case 2:
        x=2;
        y=(screen.height-imageheight)/2;
        break;
    case 3:
        x=screen.width-imagewidth-2;
        y=(screen.height-imageheight)/2;
        break;
    case 4:
        x=1;
        y=1;
        break;
    case 5:
        x=screen.width-imagewidth-2;
        y=1;
        break;
    case 6:
        x=1;
        y=screen.height-imageheight-30;
        break;
    case 7:
        x=screen.width-imagewidth-2;
        y=screen.height-imageheight-30;
        break;
}

oPopupBody = oPopup.document.body;
oPopupBody.innerHTML = st;
oPopupBody.style.cursor="pointer";
oPopupBody.title = "点击关闭";
oPopupBody.onclick=dismisspopup;
oPopupBody.oncontextmenu=dismisspopup;
pop_TC=50;
pop();
}

function pop(){
  try{
	oPopup.show(x,y,imagewidth, imageheight);
	startani_A=setTimeout("pop()",300);  //显示15秒
	if(pop_TC<0){dismisspopup();};
	pop_TC=pop_TC-1;
  }catch(e){}
}
function dismisspopup()
{	clearTimeout(startani_A);
	oPopup.hide();
}

function showgoallist(ID)
{
    window.open("http://live.nowodds.com/detail/" + ID + ".html", "", "scrollbars=yes,resizable=yes,width=668, height=720");
}
function  analysis(ID)
{
    var theURL = "http://live.nowodds.com/analysis/" + ID + ".html";
	window.open(theURL);
}
function AsianOdds(ID)
{
    var theURL = "http://live.nowodds.com/odds/match.aspx?id=" + ID;
	window.open(theURL);
}

function EuropeOdds(ID)
{
    var theURL = "http://live.nowodds.com/1x2/" + ID + ".htm";
	window.open(theURL);
}

function TeamPanlu_10(ID)
{
    var theURL = "http://live.nowodds.com/panlu/" + ID + ".html";
	window.open(theURL,"","width=640,height=700,top=10,left=100,resizable=yes,scrollbars=yes");
}
function oddsDetail(ID,cId)
{
    window.open("http://live.nowodds.com/odds/detail.aspx?scheduleID=" + ID + "&companyID=" + cId, "", "");
}

var zXml = {
    useActiveX: (typeof ActiveXObject != "undefined"),
    useXmlHttp: (typeof XMLHttpRequest != "undefined")
};
zXml.ARR_XMLHTTP_VERS = ["MSXML2.XmlHttp.6.0","MSXML2.XmlHttp.3.0"];
function zXmlHttp() {}
zXmlHttp.createRequest = function ()
{
    if (zXml.useXmlHttp)  return new XMLHttpRequest(); 
    if(zXml.useActiveX)  //IE < 7.0 = use ActiveX
    {  
        if (!zXml.XMLHTTP_VER) {
            for (var i=0; i < zXml.ARR_XMLHTTP_VERS.length; i++) {
                try {
                    new ActiveXObject(zXml.ARR_XMLHTTP_VERS[i]);
                    zXml.XMLHTTP_VER = zXml.ARR_XMLHTTP_VERS[i];
                    break;
                } catch (oError) {}
            }
        }        
        if (zXml.XMLHTTP_VER) return new ActiveXObject(zXml.XMLHTTP_VER);
    } 
    alert("对不起，您的电脑不支持 XML 插件，请安装好或升级浏览器。");
};

var flash_sound=Array(5);
flash_sound[0] = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0' width='1' height='1' id='image1'><param name='movie' value='images/sound.swf'><param name='quality' value='high'><param name='wmode' value='transparent'></object>";
flash_sound[1] = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0' width='1' height='1' id='image1'><param name='movie' value='images/notice.swf'><param name='quality' value='high'><param name='wmode' value='transparent'></object>";
flash_sound[2] = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0' width='1' height='1' id='image1'><param name='movie' value='images/base.swf'><param name='quality' value='high'><param name='wmode' value='transparent'></object>";
flash_sound[3] = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0' width='1' height='1' id='image1'><param name='movie' value='images/deep.swf'><param name='quality' value='high'><param name='wmode' value='transparent'></object>";
flash_sound[4] = "<object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0' width='1' height='1' id='image1'><param name='movie' value='images/oddsSound.swf'><param name='quality' value='high'><param name='wmode' value='transparent'></object>";

var state_ch=Array(17);
state_ch[0]="推迟,推遲,Defer";
state_ch[1]="中断,中斷,Halt";
state_ch[2]="腰斩,腰斬,Halt";
state_ch[3]="<font color=green>待定</font>,<font color=green>待定</font>,<font color=green>Wait</font>";
state_ch[13]="<b>完</b>,<b>完</b>,<b>Ft</b>";
state_ch[14]=",,";
state_ch[15]="上,上,Part1";
state_ch[16]="<font color=blue>中</font>,<font color=blue>中</font>,<font color=blue>Half</font>";
state_ch[17]="下,下,Part2";

var GoalCn="平手,平/半,半球,半/一,一球,一/球半,球半,球半/两,两球,两/两球半,两球半,两球半/三,三球,三/三球半,三球半,三球半/四,四球,四/四球半,四球半,四球半/五球,五球,五/五球半,五球半,五球半/六,六球,六球/六球半,六球半,六球半/七球,七球,七球/七球半,七球半,七球半/八球,八球,八球/八球半,八球半,八球半/九球,九球,九球/九球半,九球半,九球半/十球,十球".split(",");
function Goal2GoalCn(goal){ //数字盘口转汉汉字	
    if (goal==null || goal +""=="")
        return "";
    else{
	    if(goal>=0)  return GoalCn[parseInt(goal*4)];
	    else return "受"+ GoalCn[Math.abs(parseInt(goal*4))];
	}
}
var GoalCn2 = ["0", "0/0.5", "0.5", "0.5/1", "1", "1/1.5", "1.5", "1.5/2", "2", "2/2.5", "2.5", "2.5/3", "3", "3/3.5", "3.5", "3.5/4", "4", "4/4.5", "4.5", "4.5/5", "5", "5/5.5", "5.5", "5.5/6", "6", "6/6.5", "6.5", "6.5/7", "7", "7/7.5", "7.5", "7.5/8", "8", "8/8.5", "8.5", "8.5/9", "9", "9/9.5", "9.5", "9.5/10", "10", "10/10.5", "10.5", "10.5/11", "11", "11/11.5", "11.5", "11.5/12", "12", "12/12.5", "12.5", "12.5/13", "13", "13/13.5", "13.5", "13.5/14", "14" ];
function Goal2GoalCn2(goal){
	if (goal=="")
		return "";
	else{
		return GoalCn2[parseInt(goal*4)];
	}
}



//定义Config
var Config = new Object();
Config.language = 1;
Config.matchType = 2;
Config.oddsSound = 0;
Config.fontsize = 12;
Config.rank= 0;
Config.explain = 1;
Config.redcard = 1;
Config.detail = 1;
Config.vs = 1;
Config.odds = 1;
Config.yp = 1;
Config.op = 0;
Config.dx = 1;
Config.sound = 0;
Config.winLocation = 0;
Config.companyID=3;

Config.getCookie = function(type) {
    var Cookie=getCookie("Cookie");
    if(Cookie==null) Cookie=""; 
    var Cookie=Cookie.split("^");
    if(Cookie.length<=14) 	writeCookie("Cookie",null);
    else{
        this.language =parseInt(Cookie[0]);
        this.matchType =parseInt(Cookie[1]);
        this.oddsSound = parseInt(Cookie[2]);
        this.fontsize =parseInt(Cookie[3]);
        this.rank=parseInt(Cookie[4]);
        this.explain =parseInt(Cookie[5]);
        this.redcard =parseInt(Cookie[6]);
        this.detail =parseInt(Cookie[7]);
        this.vs =parseInt(Cookie[8]);
        this.yp =parseInt(Cookie[9]);
        this.op =parseInt(Cookie[10]);
        this.dx = parseInt(Cookie[11]);
        this.sound = parseInt(Cookie[12]);
        this.winLocation =parseInt(Cookie[13]);
    }
    if(Cookie.length==15) 	this.companyID =parseInt(Cookie[14]);
    try{
        document.getElementById("Language"+ Config.language).className="selected";
        if(this.rank==1)  document.getElementById("rank").checked=true;
        if(this.explain==0)  document.getElementById("explain").checked=false;
        if(this.redcard==0)  document.getElementById("redcard").checked=false;
        if(this.detail==0)  document.getElementById("detail").checked=false;
        if(this.vs==0)  document.getElementById("vs").checked=false;
        if(this.sound==-1)  document.getElementById("soundCheck").checked=false;
        if(this.sound>0)  document.getElementById("sound").value=this.sound;
        if(this.oddsSound==1)  document.getElementById("oddsSound").checked=true;
        if(this.winLocation==-1)  document.getElementById("windowCheck").checked=false;
        if(this.winLocation>0)  document.getElementById("winLocation").value=this.winLocation;
        if(type=="2in1"){
            if(this.yp==1)  document.getElementById("yp").checked=true;
            if(this.op==1)  document.getElementById("op").checked=true;
            if(this.dx==1)  document.getElementById("dx").checked=true;
        }
        else
            document.getElementById("MatchType"+ Config.matchType).className="selected";
    }
    catch(e){}
}

Config.writeCookie = function() {
    var value=this.language+"^" + this.matchType+"^" +this.oddsSound +"^" + this.fontsize +"^" +  this.rank+"^" + this.explain +"^" + this.redcard +"^" + this.detail+"^" + this.vs+"^" + this.yp +"^" + this.op +"^" + this.dx +"^" +this.sound +"^" + this.winLocation +"^" + this.companyID;    
    writeCookie("Cookie",value);
}