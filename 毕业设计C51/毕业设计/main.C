//********************************************************************************************************************************//
//************************************光照度语音播报系统*************************************************************************//
//************************************作者   曹阳*********************************************************************************//
//************************************时间   2014/7/1*****************************************************************************//
//********************************************************************************************************************************//
#include <reg52.h>
#include <intrins.h>
#include "gglj.h"
#include "sound.h"
#include "ISD1700.H"
	
#define uchar unsigned char
#define uint  unsigned int

sbit ISD_SS=P2^7;
sbit ISD_MISO=P2^3;
sbit ISD_MOSI=P2^5;
sbit ISD_SCLK=P2^6;

uchar buf;

int flag=0;


#define delayNOP(); {_nop_();_nop_();_nop_();_nop_();};
uchar IRDIS[2];
uchar IRCOM[4];


/*********函数声明**************/
extern void  spi_pu (void);
extern void  comm_sate(void);
extern void  spi_stop (void);
extern void  spi_fwd (void);
extern void  spi_play(void);
extern void  isd1700_7byte_comm(uchar comm_par, uint star_addr, uint end_addr);
void  init(void);
void  PlaySoundTick(uchar  number);
void  LEDShow(void);
void  delay_isd(uint time);
void GetSound(uchar soundtick);


extern uchar get8adz();	



static void mydelay(uint z)
{
     uint x,y;
	 for(x=z;x>0;x--)
	    for(y=110;y>0;y--);

}
/*******************************************************************/
/*                                                                 */
/*  延时函数                                                       */
/*                                                                 */
/*******************************************************************/
void delay(int ms)
{
    while(ms--)
	{
      uchar i;
	  for(i=0;i<250;i++)  
	   {
	    _nop_();			   
		_nop_();
		_nop_();
		_nop_();
	   }
	}
}		

/*********************************************************/
/*														 */
/* 主程序           									 */
/*                                                       */
/*********************************************************/
main()
{
	uchar i;

	SCON=0x50;           //设定串口工作方式
    PCON=0x00;           //波特率不倍增
			
    TMOD=0x20;           //定时器1工作于8位自动重载模式, 用于产生波特率
    EA=1;
    ES = 1;              //允许串口中断
    TL1=0xfd;
    TH1=0xfd;             //波特率9600
    TR1=1;


	delay(10);                 //延时
	init();				//器件初始化          
	GetSound(0);
	spi_play();
   	for(i=0;i<8;i++)
	{
		delay(1000); 
	}
	flag=0;


	while(1)
	{
	
		if(1 == flag) //解锁成功
		{
			GetSound(1);
			spi_play();
			delay(1000); 
			delay(1000); 
			flag=0;

		}
		else if(2 == flag)//报警
		{
			GetSound(2);
			spi_play();	
			for(i=0;i<3;i++)
			{
				delay(1000); 
			}	
		}
			
	}
}



/*********************************************************/
/*														 */
/* 延时x*0.14ms子程序									 */
/*                                                       */
/*********************************************************/

void delay0(uchar x)    //x*0.14MS
{
  uchar i;
  while(x--)
 {
  for (i = 0; i<13; i++) {}
 }
}

void  init(void)
{	    
	spi_pu();	
}



void delay_isd(uint time)
{
	while(time--!=0);
}

/**************获取指定语音段地址并播放，用户可根据实际需要进行增减******************/
/**************对应的语音段地址在SOUND.H文件里，具体地址从录音软件中读取*************/

void GetSound(uchar soundtick)
{
	ISD_SS=0;
	switch(soundtick)
	{  
		case 0:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_0A, sound_0B); }break;
		case 1:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_1A, sound_1B); }break;
	    case 2:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_2A, sound_2B); }break;
	    case 3:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_3A, sound_3B); }break;
	    case 4:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_4A, sound_4B); }break;
	    case 5:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_5A, sound_5B); }break;
	    case 6:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_6A, sound_6B); }break;
	    case 7:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_7A, sound_7B); }break;
	    case 8:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_8A, sound_8B); }break;
	    case 9:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_9A, sound_9B); }break;
	    case 10:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_10A, sound_10B); }break;
	    case 11:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_11A, sound_11B); }break;
		case 12:{ isd1700_7byte_comm(ISD1700_SET_PLAY|ISD_LED, sound_12A, sound_12B); }break;
	    default: break;
     }
	ISD_SS=1;
}

/**********播放指定语音段************/
void PlaySoundTick(uchar  number)
{
	  spi_stop ();
	  delay_isd(30000);
      GetSound(number);
}

/*********************************************************

  串行中断服务函数

*********************************************************/
void  serial() interrupt 4 
{
   ES = 0;                //关闭串行中断
   RI = 0;                //清除串行接受标志位
   buf = SBUF;            //从串口缓冲区取得数据
   flag=1;
  switch(buf)
   {
      case 0x31:  P1&=0xfe;;break;  //接受到1，第一个LED亮         
      case 0x32:  P1&=0xfd;break;  //接受到2，第二个LED亮        
      case 0x33:  P1&=0xfb;break;  //接受到3，第三个LED亮        
      case 0x34:  P1&=0xf7;break;  //接受到4，第四个LED亮       
      case 0x35:  P1&=0xef;break;  //接受到5，第五个LED亮            
      case 0x36:  P1&=0xdf;break;  //接受到6，第六个LED亮                   
      case 0x37:  P1&=0xbf;break;  //接受到7，第七个LED亮
	  case 0x38:  P1&=0x7f;break;  //接受到8，第八个LED亮
	  case 0x39:  flag=2;break;
	  default:    flag=1;break;       
   }
   ES = 1;    //允许串口中断
}

