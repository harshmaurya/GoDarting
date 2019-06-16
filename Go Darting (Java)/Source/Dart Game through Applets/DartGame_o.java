/*
 * <applet code="DartGame" width=800 height=600>
 * </applet>
 */
import java.applet.*;
import java.awt.*;
import java.awt.event.*;
import static java.lang.Math.sqrt;
import static java.lang.Math.pow;

public class DartGame extends Applet implements Runnable, MouseListener{
Font f;
Image img,img1,img2,hptr,vptr,board,dart,itotscore,levelimage,gameover;
Image iscore[]=new Image[3];
Thread t=null,t1=null;  //Seperate Thread to play music
int scene=1,mouseX=0,mouseY=0,hpt=0,vpt=0,count=0,dx,dy,turn,hdis,vdis,mode=1;
Integer score[]=new Integer[3];
Integer totscore,level,finalscore=0;
boolean mouseclk=false,tstart=false,levelbreak=false;
public void init() {
	addMouseListener(this);
	img=getImage(getDocumentBase(),"res\\back.png");
	img1=getImage(getDocumentBase(),"res\\but.png");
	img2=getImage(getDocumentBase(),"res\\back2.png");
	hptr=getImage(getDocumentBase(),"res\\hpointer.png");
	vptr=getImage(getDocumentBase(),"res\\vpointer.png");
	board=getImage(getDocumentBase(),"res\\board.png");
	dart=getImage(getDocumentBase(),"res\\dart.png");
	gameover=getImage(getDocumentBase(),"res\\gameover.png");
	f=new Font("Serif",Font.BOLD,24);
	setFont(f);	
}

public void start() {
t=new Thread(this);
t.start();
t1=new Thread(this);
}

public void paint (Graphics g) {
Color c1,c2;
c1=new Color(15,198,209); // RGB values	
c2=new Color(29,142,201);
setBackground(c2);
switch(scene) {

	case 1:
	g.drawImage(img,0,0,this);
	g.drawImage(img1,550,400,this);
	break;
	
	case 2:
	if(tstart==false)
	{
	t1.start();
	tstart=true;
	}
	if(mode==1) {
	g.drawImage(levelimage,300,200,this);
	}
	else if(mode==2){
	g.drawImage(img2,0,0,this);
	g.drawImage(board,300,20,this);
	g.drawString(new String("Level clears at "+(150-level*10)+" points"),10,530);
	g.setColor(c1);
	g.setColor(Color.red);
	g.drawImage(hptr,200+hpt,530,this);
	g.drawImage(vptr,730,100+vpt,this);
	if(dx!=0)
	g.drawImage(dart,dx+hdis,dy+vdis,this);
	if(itotscore!=null)
	g.drawImage(itotscore,50,470,this);
	for(int i=0;i<3;++i) {
	if(iscore[i]!=null)
	g.drawImage(iscore[i],50,(150+100*i),this);
	
}
}
	else if(mode==3) {
	g.drawImage(gameover,250,200,this);
	g.drawString(new String("Your Final Score: "+finalscore),300,450);
}
break;	
 
}

}

// Entry point for thread playing music
public void run() {
switch(scene) {
case 1:
AudioClip ac;
ac=getAudioClip(getDocumentBase(),"res\\music.wav");
ac.loop();
break;
case 2:

for(level=1;level<=9;++level)
{
mode=1; //level seeing mode
levelimage=getImage(getDocumentBase(),new String("res\\"+level.toString()+".png"));
repaint();
try {
Thread.sleep(2000);
}
catch ( Exception e) {
e.printStackTrace();
}
mode=2; // playing mode
for(int i=0;i<3;++i) {
iscore[i]=null;
}
itotscore=null;
repaint();
totscore=0;
for(turn=0;turn<3;++turn) {
hpt=0;vpt=0;count=0;mouseclk=false;dx=0;dy=0;hdis=300;vdis=300;
repaint(200,530,400,60);
repaint(730,100,60,400);
repaint(dx,dy,85,70);
while(true) {
if(mouseclk==true)
break;
if(count<40)
	hpt+=10;
	else if(count>=40 && count<79)
	hpt-=10;
	else
	count=0;
	count++;
	repaint(200+hpt-20,530,60,30);
try {
	Thread.sleep(20-level*2);
	}
	catch (Exception e) {
	e.printStackTrace();
	}

}
mouseclk=false;
count=0;
while(true) {
if(mouseclk==true)
break;
if(count<40)
	vpt+=10;
	else if(count>=40 && count<79)
	vpt-=10;
	else
	count=0;
	count++;
	repaint(730,100+vpt-20,30,60);
try {
	Thread.sleep(20-level*2);
	}
	catch (Exception e) {
	e.printStackTrace();
	}

}
dx=(int)(hpt*0.5+300);
dy=(int)(vpt*0.5+20);
for(;;) {
repaint();
hdis-=20;
vdis-=20;
if(hdis==0 || vdis==0)
break;
try {
Thread.sleep(20);
}
catch ( Exception e) {
e.printStackTrace();
}
}
try {
Thread.sleep(1000);
}
catch ( Exception e) {
e.printStackTrace();
}
score[turn]=(int)(100-sqrt(pow(400-dx,2)+pow(120-dy,2)));
score[turn]/=10;
score[turn]*=10;
if(score[turn]<0)
score[turn]=0;
iscore[turn]=getImage(getDocumentBase(),new String("res\\"+score[turn].toString()+".jpg"));
repaint(50,(150+100*turn),90,70);
try {
Thread.sleep(1000);
}
catch ( Exception e) {
e.printStackTrace();
}

totscore+=score[turn];
}
itotscore=getImage(getDocumentBase(),new String("res\\"+totscore.toString()+".jpg"));
repaint(50,470,180,50);
finalscore+=totscore;
try {
Thread.sleep(2000);
}
catch ( Exception e) {
e.printStackTrace();
}
if(totscore<=(150-level*10)) {
mode=3; // Game over mode
repaint();
try {
Thread.sleep(2000);
}
catch ( Exception e) {
e.printStackTrace();
}
break;

}

}
break;
}
}
public void mouseClicked(MouseEvent me) {
mouseX=me.getX();
mouseY=me.getY();
switch (scene) {
case 1:
if(mouseX>550 && mouseX<750 && mouseY>400 && mouseY<500) {
scene++;
repaint();
}
break;
case 2:
mouseclk=true;

break;
}

}
public void mouseEntered(MouseEvent me) {
}
public void mouseExited(MouseEvent me) {
}
public void mousePressed(MouseEvent me) {
}
public void mouseReleased(MouseEvent me) {
}

}
 