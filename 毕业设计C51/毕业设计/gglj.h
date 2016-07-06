#include <reg52.h>
#ifndef _gglj_h
#define _gglj_h

#define uchar unsigned char
#define uint unsigned int
//=====================================
void delayms(uint n);
void delayus(uint n);

#define LCDCOM P0
//sbit RS=P2^5;
//sbit RW=P2^6;
//sbit EN=P2^7;

//void writecom(uchar com);
//void writedata(uchar dat);
//void writestr(uchar *p);
//void lcdinit(void);
//=====================================
sbit DQ=P3^5;//sdo
sbit CS=P3^6;//
sbit CLK=P3^4;
//uchar get8adz();
//uint getaveadz(uint n);

//=====================================
//sbit AA=P1^5;
//sbit BB=P1^6;
//sbit CC=P1^7;
//sbit EN0=P1^4;

//void zyswitch(uchar n);
//
////=====================================
//
//extern uchar sch0;
//static uchar ledflag;
//extern long x;
////=====================================
//void play();
//void initplay();
//uint getswitchsum();
#endif
