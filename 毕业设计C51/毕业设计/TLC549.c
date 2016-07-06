#include "gglj.h"

void delayus(uint n)
{
	int i=12,j;
	for(j=0;j<n;j++)
		for(i=12;i>0;i--);

}
//====================================
//读8位AD值
//====================================
uchar get8adz()				
{
	uchar i,temp;
	CS=1;
	CLK=0;
	CS=0;
	for(i=0;i<8;i++)
	{
		temp=temp<<1;
		temp=temp|DQ;
		CLK=1;
		delayus(10);
		CLK=0;
		delayus(10);	
	}
	CS=1;
	delayus(20);
	return (temp);
}
//===================================
//采N次AD值
//===================================
//uint getaveadz(uint n)		  
//{
//	uint num;
//	uint i;
//	num=0;
//	for(i=0;i<n;i++)
//	{
//	 	num=num+get8adz();
//	}
//	return num;
//}
//===================================
//自动量程转换
//===================================
//uint getswitchsum()		    
//{
//	uint sum;
//				   
//	sch0=1;
//	zyswitch(sch0);
//	delayms(10);
//	sum=(uint)getaveadz(10);
//	if(sum<400) 
//		{
//			sch0=2;
//			zyswitch(sch0);		    
//			delayms(5);
//			sum=(uint)getaveadz(10);
//			if(sum<400)
//			{
//				sch0=3;
//				zyswitch(sch0);		    
//				delayms(5);
//				sum=(uint)getaveadz(10);
//			}
//
//		}
//	 return sum;
//}
