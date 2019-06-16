import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.awt.image.BufferedImage;
import java.io.InputStream;
import java.util.*;
import javax.sound.midi.*;
import javax.imageio.ImageIO;
import static java.lang.Math.sqrt;
import static java.lang.Math.pow;

//This class creates a thread to play music
class MusicPlay implements Runnable {
	static Sequencer midiPlayer;
	InputStream actual;
	MusicPlay(){		
		
		Thread music=new Thread(this);
		music.start();
	}
	public void run()
	{
		try {
			InputStream in00=getClass().getResourceAsStream("music.MID");
			InputStream in01=getClass().getResourceAsStream("music2.MID");
			InputStream in02=getClass().getResourceAsStream("music3.MID");
			InputStream in03=getClass().getResourceAsStream("music4.MID");
		 Random rand = new Random();
    		 int rannum = rand.nextInt(4);
		 switch (rannum) {

		case 0: actual=in00;
			break;
		case 1: actual=in01;
			break;
		case 2: actual=in02;
			break;
		case 3: actual=in03;
			break;
		default: actual=in00;
			break;


	}
	         Sequence song = MidiSystem.getSequence(actual);
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

public class DartGame extends JPanel implements Runnable, MouseListener, ActionListener{
	
	Font f;
	JPanel cn=new JPanel();
	JLabel lb=new JLabel("Select your tartget");
	JFrame jf;
	JButton b1,b2,b3,b4;
	int scene=1,mouseX=0,mouseY=0,hpt=0,vpt=0,count=0,dx,dy,turn,hdis,vdis,mode=0;
	Integer score[]=new Integer[3];
	Integer totscore,level,finalscore=0;
	boolean mouseclk=false,tstart=false,levelbreak=false;
	BufferedImage img,img1,img2,hptr,vptr,board,dart,itotscore,levelimage,gameover,fkoff;
	BufferedImage iscore[]=new BufferedImage[3];
	ImageIcon im1,im2,im3,im4;
	InputStream  in1,in2,in3,in4,in5,in6,in7,in8,in9,in20,in21,in22,in23,in24,in25,in26,in27,in28;
	
	public DartGame(JFrame jf) {
		
	this.jf=jf;
	Color c2;
	c2=new Color(29,142,201);
	jf.setBackground(c2);
	jf.getContentPane().add(this);
	jf.setSize(820,650);
	jf.setVisible(true);
	
	jf.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	
	
		
		try {
			in1=getClass().getResourceAsStream("back.png");
			in2=getClass().getResourceAsStream("but.png");
			in3=getClass().getResourceAsStream("back2.png");
			in4=getClass().getResourceAsStream("hpointer.png");
			in5=getClass().getResourceAsStream("vpointer.png");
			in6=getClass().getResourceAsStream("boardobama.png");
			in7=getClass().getResourceAsStream("dart.png");
			in8=getClass().getResourceAsStream("fkoffobama.png");
			in9=getClass().getResourceAsStream("gameover.png");
			in20=getClass().getResourceAsStream("boardbrad.png");
			in21=getClass().getResourceAsStream("fkoffbrad.png");
			in22=getClass().getResourceAsStream("boardsymonds.png");
			in23=getClass().getResourceAsStream("fkoffsymonds.png");
			in24=getClass().getResourceAsStream("boardkohli.png");
			in25=getClass().getResourceAsStream("fkoffkohli.png");
		img=ImageIO.read(in1);
		img1=ImageIO.read(in2);
		img2=ImageIO.read(in3);
		hptr=ImageIO.read(in4);
		vptr=ImageIO.read(in5);
		
		dart=ImageIO.read(in7);
		
		gameover=ImageIO.read(in9);
		im1=new ImageIcon(getClass().getResource("obama.png"));
		im2=new ImageIcon(getClass().getResource("brad.png"));
		im3=new ImageIcon(getClass().getResource("symonds.png"));
		im4=new ImageIcon(getClass().getResource("kohli.png"));
		}
		catch (Exception e) {
			e.printStackTrace();
		}
		f=new Font("Serif",Font.BOLD,24);
		setFont(f);	
		
		b1=new JButton("Barack Obama",im1);
		b1.setActionCommand("first");
		b1.addActionListener(this);
		b2=new JButton("Brad Pitt    ",im2);
		b2.setActionCommand("second");
		b2.addActionListener(this);
		b3=new JButton("Andrew Symonds",im3);
		b3.setActionCommand("third");
		b3.addActionListener(this);
		b4=new JButton("Virat Kohli  ",im4);
		b4.setActionCommand("fourth");
		b4.addActionListener(this);
		cn.setLayout(new GridBagLayout());
		cn.add(lb);
		cn.add(b1);
		cn.add(b2);
		cn.add(b3);
		cn.add(b4);
		

		

	}
	
	public static void main(String args[]) {
		Thread t1=null;
		// wait for the splash screen
		try {
		Thread.currentThread().sleep(4000);
		}
		catch(Exception e) {
		}
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
			
			else if(mode==5) {
				jf.getContentPane().add(cn);
				jf.setVisible(true);
				
			}
			
			else {
				g.drawString("Congratulations !! You have beat this game..",200,200);
			}
			

		}

	


	public void run() {
		jf.addMouseListener(this);
		// Do nothing till user clicks on start. When he does that, mode becomes 1
		while(mode==0) {
			
		}
		try {
		Thread.sleep(1000);
		}
		catch(Exception e) {
			
		}
		while(mode==5) {
			
		}
		
		
			for(level=1;level<=9;++level)
			{
				
				mode=1; //level seeing mode
				try {
					InputStream in10=getClass().getResourceAsStream(new String(level.toString()+".png"));
				levelimage=ImageIO.read(in10);
				}
				catch (Exception e) {
					
				}
				jf.repaint();
				try {
					Thread.sleep(1500);
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
						jf.repaint();
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
					InputStream in11=getClass().getResourceAsStream(new String(score[turn].toString()+".jpg"));
					iscore[turn]=ImageIO.read(in11);
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
				InputStream in12=getClass().getResourceAsStream(new String(totscore.toString()+".jpg"));
				itotscore=ImageIO.read(in12);
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
	
	public void actionPerformed(ActionEvent ae) {
		
				if(ae.getActionCommand().equals("first")){
					try {
					board=ImageIO.read(in6);
					fkoff=ImageIO.read(in8);
					}
					catch(Exception e) {
					e.printStackTrace();	
					}
					
					jf.getContentPane().remove(cn);
					mode=1;
					jf.repaint();
					
				}
				
				else if(ae.getActionCommand().equals("second")){
					try {
					board=ImageIO.read(in20);
					fkoff=ImageIO.read(in21);
					}
					catch(Exception e) {
					e.printStackTrace();	
					}
					
					jf.getContentPane().remove(cn);
					mode=1;
					jf.repaint();
					
				}
				
				else if(ae.getActionCommand().equals("third")){
					try {
					board=ImageIO.read(in22);
					fkoff=ImageIO.read(in23);
					}
					catch(Exception e) {
					e.printStackTrace();	
					}
					
					jf.getContentPane().remove(cn);
					mode=1;
					jf.repaint();
					
				}
				
				else if(ae.getActionCommand().equals("fourth")){
					try {
					board=ImageIO.read(in24);
					fkoff=ImageIO.read(in25);
					}
					catch(Exception e) {
					e.printStackTrace();	
					}
					
					jf.getContentPane().remove(cn);
					mode=1;
					jf.repaint();
					
				}
				
			}
	
	
	public void mouseClicked(MouseEvent me) {
		mouseX=me.getX();
		mouseY=me.getY();
			if(mode==0) {			// user has clicked on start
			if(mouseX>550 && mouseX<750 && mouseY>400 && mouseY<500) {
				mode=5;
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
