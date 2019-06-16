import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.awt.image.BufferedImage;
import java.io.File;
import javax.sound.midi.*;
import javax.imageio.ImageIO;
import static java.lang.Math.sqrt;
import static java.lang.Math.pow;

//This class creates a thread to play music
class MusicPlay implements Runnable {
	static Sequencer midiPlayer;
	
	MusicPlay(){		
		
		Thread music=new Thread(this);
		music.start();
	}
	public void run()
	{
		try {
			
			String midFilename=new String(System.getProperty("user.dir")+"\\res\\music.MID");
	         File midiFile = new File(midFilename);
	         Sequence song = MidiSystem.getSequence(midiFile);
	         midiPlayer = MidiSystem.getSequencer();
	         midiPlayer.open();
	         midiPlayer.setSequence(song);
	         midiPlayer.setLoopCount(Sequencer.LOOP_CONTINUOUSLY); // repeat
	         midiPlayer.start();
	      } catch (Exception e) {
	         e.printStackTrace();
	      } 
		
	}
}

public class DartGame extends JPanel implements Runnable, MouseListener{
	
	Font f;
	
	JFrame jf;
	int scene=1,mouseX=0,mouseY=0,hpt=0,vpt=0,count=0,dx,dy,turn,hdis,vdis,mode=0;
	Integer score[]=new Integer[3];
	Integer totscore,level,finalscore=0;
	boolean mouseclk=false,tstart=false,levelbreak=false;
	BufferedImage img,img1,img2,hptr,vptr,board,dart,itotscore,levelimage,gameover,fkoff;
	BufferedImage iscore[]=new BufferedImage[3];
	public DartGame(JFrame jf) {
		
	this.jf=jf;
	Color c2;
	c2=new Color(29,142,201);
	jf.setBackground(c2);
	jf.getContentPane().add(this);
	jf.setSize(820,650);
	jf.setVisible(true);
	jf.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	
		addMouseListener(this);
		try {
			
		img=ImageIO.read(new File(new String(System.getProperty("user.dir")+"\\res\\back.png")));
		img1=ImageIO.read(new File(new String(System.getProperty("user.dir")+"\\res\\but.png")));
		img2=ImageIO.read(new File(new String(System.getProperty("user.dir")+"\\res\\back2.png")));
		hptr=ImageIO.read(new File(new String(System.getProperty("user.dir")+"\\res\\hpointer.png")));
		vptr=ImageIO.read(new File(new String(System.getProperty("user.dir")+"\\res\\vpointer.png")));
		board=ImageIO.read(new File(new String(System.getProperty("user.dir")+"\\res\\board.png")));
		dart=ImageIO.read(new File(new String(System.getProperty("user.dir")+"\\res\\dart.png")));
		fkoff=ImageIO.read(new File(new String(System.getProperty("user.dir")+"\\res\\fkoff.png")));
		gameover=ImageIO.read(new File(new String(System.getProperty("user.dir")+"\\res\\gameover.png")));
		}
		catch (Exception e) {
			e.printStackTrace();
		}
		f=new Font("Serif",Font.BOLD,24);
		setFont(f);	
		
	

	
		

	}
	
	public static void main(String args[]) {
		Thread t1=null;
		JFrame jf=new JFrame("Dart Game By Harsh Maurya");
		DartGame dg= new DartGame(jf);
		new MusicPlay();
		t1=new Thread(dg);
		t1.start();
	}
	
	public void paintComponent (Graphics g) {
		super.paintComponents(g);
		
		
				// startup image	
			if(mode==0) {
				g.drawImage(img,0,0,this);
				g.drawImage(img1,550,400,this);
				}
			
			// level seeing
			else if(mode==1) {
				g.drawImage(levelimage,300,200,this);
			}
			
			//playing mode
			else if(mode==2){
				g.drawImage(img2,0,0,this);
				g.drawImage(board,300,20,this);
				g.drawString(new String("Level clears at "+(150-level*10)+" points"),10,530);
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
			
			//Gameover
			else if(mode==3) {
				g.drawImage(gameover,250,200,this);
				g.drawString(new String("Your Final Score: "+finalscore),300,450);
			}
			
			
			else if(mode==4){
				g.drawImage(fkoff,0,0,this);
			}
			else {
				g.drawString("Congratulations !! You have beat this game..",200,200);
			}
			

		}

	


	public void run() {
		
		// Do nothing till user clicks on start. When he does that, mode becomes 1
		while(mode==0) {
			
		}
		
			for(level=1;level<=9;++level)
			{
				mode=1; //level seeing mode
				try {
				levelimage=ImageIO.read(new File(System.getProperty("user.dir"),new String("res\\"+level.toString()+".png")));
				}
				catch (Exception e) {
					
				}
				jf.repaint();
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
				jf.repaint();
				totscore=0;
				for(turn=0;turn<3;++turn) {
					hpt=0;vpt=0;count=0;mouseclk=false;dx=0;dy=0;hdis=300;vdis=300;
					jf.repaint(200,530,400,60);
					jf.repaint(730,100,60,400);
					jf.repaint(dx,dy,85,70);
					// move the horizontal pointer
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
						jf.repaint();
						try {
							Thread.sleep(20-level*2); // This decides the speed of horizontal pointer
						}
						catch (Exception e) {
							e.printStackTrace();
						}

					}
					mouseclk=false;
					count=0;
					// move the vertical pointer
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
						jf.repaint();
						try {
							Thread.sleep(20-level*2); // This decides the speed of vertical pointer
						}
						catch (Exception e) {
							e.printStackTrace();
						}

					}
					
					//determine the hitting point of dart in the dartboard using horizontal and vertical pointers
					dx=(int)(hpt*0.5+300);
					dy=(int)(vpt*0.5+20);
					
					// animation to show dart hitting the dartboard
					for(;;) {
						jf.repaint(dx+hdis,dy+vdis,100,100);
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
					jf.repaint(dx+hdis,dy+vdis,100,100);
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
					try {
					iscore[turn]=ImageIO.read(new File(System.getProperty("user.dir"),new String("res\\"+score[turn].toString()+".jpg")));
					}
					catch (Exception e) {
						
					}
					jf.repaint(50,(150+100*turn),90,70);
					try {
						Thread.sleep(1000);
					}
					catch ( Exception e) {
						e.printStackTrace();
					}

					totscore+=score[turn];
				}
				try {
				itotscore=ImageIO.read(new File(System.getProperty("user.dir"),new String("res\\"+totscore.toString()+".jpg")));
				}
				catch (Exception e) {
					
				}
				jf.repaint(50,470,180,150);
				finalscore+=totscore;
				try {
					Thread.sleep(2000);
				}
				catch ( Exception e) {
					e.printStackTrace();
				}
				if(totscore<(150-level*10)) {
					mode=4;
					jf.repaint();
					try {
						Thread.sleep(3000);
					}
					catch ( Exception e) {
						e.printStackTrace();
					}
					mode=3; // Game over mode
					jf.repaint();
					try {
						Thread.sleep(2000);
					}
					catch ( Exception e) {
						e.printStackTrace();
					}
					break;

				}

			}
			if(level==10)
			{mode=5;jf.repaint();}
			try {
			Thread.sleep(3000);
			}
			catch (Exception e) {
				e.printStackTrace();
			}
			System.exit(0);
			
		
	}
	public void mouseClicked(MouseEvent me) {
		mouseX=me.getX();
		mouseY=me.getY();
			if(mode==0) {			// user has clicked on start
			if(mouseX>550 && mouseX<750 && mouseY>400 && mouseY<500) {
				mode++;
				jf.repaint();
				
			}
			}
			else 					// user has clicked the mouse to stop the pointer
				mouseclk=true;
			
		
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
