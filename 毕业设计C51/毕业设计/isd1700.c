//#pragma src
#include "reg51.h"
#include "ISD1700.H"
#include "sound.h"
#define  uchar unsigned char 
#define  uint  unsigned int

sbit ISD_SS=P2^7;
sbit ISD_MISO=P2^3;
sbit ISD_MOSI=P2^5;
sbit ISD_SCLK=P2^6;


bit  re_fig;
uchar data ISD_COMM_RAM[7];
uchar data ISD_COMM_RAM_C[7];
uchar data *isd_comm_ptr;
uchar data *back_data_ptr;

void  init(void);
static void  delay(int x);
void  comm_sate(void);
void  rest_isd_comm_ptr(void);
uchar T_R_comm_byte( uchar comm_data );
void isd1700_par2_comm(uchar comm_par, uint data_par);
void isd1700_Npar_comm(uchar comm_par,comm_byte_count); //no parameter comm
void isd1700_7byte_comm(uchar comm_par, uint star_addr, uint end_addr);

void  spi_pu (void);
void  spi_stop (void);
void  spi_Rest ( void );
void  spi_CLR_INT(void);
void  spi_RD_STAUS(void);
void  spi_RD_play_ptr(void);
void  spi_pd(void);
void  spi_RD_rec_ptr(void);
void  spi_devid(void);
void  spi_play(void);
void  spi_rec (void);
void  spi_erase (void);
void  spi_G_ERASE (void);
void  spi_rd_apc(void);
void  spi_wr_apc1 (void);
void  spi_wr_apc2 (void);
void  spi_wr_nvcfg (void);
void  spi_ld_nvcfg (void);
void  spi_fwd (void);
void  spi_chk_mem(void);
void  spi_CurrRowAddr(void);

void  seril_back_sate(uchar byte_number);
void  spi_set_opt(uchar spi_set_comm);

//串口通信接口函数
void  comm_sate(void)
      {
        uchar sate_temp;
		uint  apc_temp;		
	    if(RI)					//接收到命令
		  { sate_temp=SBUF;		//下面根据不同的命令执行不同的函数
		    
			if(sate_temp==0x09)
              { spi_devid();}
              
            if(sate_temp==0x44)
			  {spi_rd_apc();}

            if(sate_temp==0x40)
			  {spi_play();}
			
  			  if(sate_temp==0x04)
			  {spi_CLR_INT();}

			  if(sate_temp==0x05)
			  {spi_RD_STAUS();}

			  if(sate_temp==0x43)
			  { spi_G_ERASE();}

              if(sate_temp==0x01)
              { spi_pu ();}
               
			  if(sate_temp==0x02)
              { spi_stop();}
              
			  if(sate_temp==0x03)
              { spi_Rest ();}
			  

            if(sate_temp==0x06)
			  {spi_RD_play_ptr();}

            if(sate_temp==0x07)
			  {spi_pd();}

			  if(sate_temp==0x08)
			  { spi_RD_rec_ptr();}

			  if(sate_temp==0x41)
			  { spi_rec();}


			  if(sate_temp==0x42)
              { spi_erase();}
              
            if(sate_temp==0x45)
			  {spi_wr_apc1 ();}

            if(sate_temp==0x65)
			  { spi_wr_apc2 ();}
			
  			  if(sate_temp==0x46)
			  { spi_wr_nvcfg ();}

			  if(sate_temp==0x47)
			  { spi_ld_nvcfg ();}

			  if(sate_temp==0x48)
			  { spi_fwd ();}

			  if(sate_temp==0x49)
              { spi_chk_mem();}

			  if(sate_temp==0x60)
                { spi_CurrRowAddr();}
              
            if(sate_temp==0x80)
			  {   
                spi_set_opt(ISD1700_SET_PLAY|ISD_LED); 
				//spi_set_opt(ISD1700_SET_PLAY);
			  }

            if(sate_temp==0x81)
			  {
			    spi_set_opt(ISD1700_SET_REC|ISD_LED);
				//spi_set_opt(ISD1700_SET_REC);
                ISD_COMM_RAM_C[0]=ISD1700_SET_REC ;
                seril_back_sate(1);
			  }
			
  			  if(sate_temp==0x82)
			  {
			    spi_set_opt(ISD1700_SET_ERASE|ISD_LED);
				//spi_set_opt(ISD1700_SET_ERASE);
			  }

              if(sate_temp==ISD1700_WR_APC2)
		       {  
   			      RI=0;
				  while(!RI);
                  apc_temp=SBUF;
				  apc_temp=apc_temp<<8;
				  RI=0;
                  while(!RI);
                  apc_temp|=SBUF;
				  RI=0;
                  ISD_SS=0;
                  isd1700_par2_comm(ISD1700_WR_APC2,apc_temp);
                  ISD_SS=1;
               }

		    RI=0;		   
		  }
		if(re_fig)
		  { 
		    rest_isd_comm_ptr();
			sate_temp=0;
			do{
		        SBUF=*back_data_ptr++;
		        while(!TI);
			    TI=0;
			   }while(++sate_temp<=2);
            re_fig=0;
		  }
	   }



/******************************************************************/
/*******以下为ISD各子功能函数，详细请查阅ISD语音芯片数据手册******/

//设置函数
void  spi_set_opt(uchar spi_set_comm)
      {   
	      uint  start_addr,end_addr;
          RI=0;
		  while(!RI);
          start_addr=SBUF;
		  start_addr=start_addr<<8;
		  RI=0;
          while(!RI);
          start_addr|=SBUF;
	      RI=0;

		  while(!RI);
          end_addr=SBUF;
		  end_addr=start_addr<<8;
		  RI=0;
          while(!RI);
          end_addr|=SBUF;
		  RI=0;
                  
          ISD_SS=0;
          isd1700_7byte_comm(spi_set_comm, start_addr, end_addr);
          ISD_SS=1;  
       	}			  

//器件上电
void  spi_pu (void)
      {  
		 ISD_SS=0;
         isd1700_Npar_comm(ISD1700_PU,2);    
		 ISD_SS=1;
      }

//器件停止
void  spi_stop (void)
      {  
		 ISD_SS=0;
         isd1700_Npar_comm(ISD1700_STOP,2);     
		 ISD_SS=1;
         ISD_COMM_RAM_C[0]=ISD1700_STOP ;
         seril_back_sate(1);
      }
//器件复位
void  spi_Rest (void)
       {
         ISD_SS=0;
         isd1700_Npar_comm(ISD1700_REST,2);    
		 ISD_SS=1;
	   }

//清楚EOM标志
void  spi_CLR_INT(void) 
       {   
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_CLR_INT,2);     
		    ISD_SS=1;
	     }

//器件状态
void  spi_RD_STAUS(void)
      {     uchar i;
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_RD_STAUS,3);   
		    ISD_SS=1;
            i=ISD_COMM_RAM_C[1];                    
			//j=ISD_COMM_RAM_C[2];
            ISD_COMM_RAM_C[1]=ISD_COMM_RAM_C[0];   
            ISD_COMM_RAM_C[0]=i;
            seril_back_sate(3);
       }

//当前地址
void  spi_CurrRowAddr(void)
       {    uchar i;
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_RD_STAUS,3); 
		    ISD_SS=1;
			i=ISD_COMM_RAM_C[1];
            ISD_COMM_RAM_C[1]=ISD_COMM_RAM_C[0]>>5|ISD_COMM_RAM_C[1]<<3;  
            ISD_COMM_RAM_C[0]= i >>5;   
            seril_back_sate(3);
	   }

//读播放地址
void  spi_RD_play_ptr(void)
       {    uchar i;
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_RD_PLAY_PTR,4);      
		    ISD_SS=1;			
			i=ISD_COMM_RAM_C[3]&0x03;               
            ISD_COMM_RAM_C[3]=ISD_COMM_RAM_C[2];    
			ISD_COMM_RAM_C[2]=i;
			seril_back_sate(4);
	   }

//器件掉电
void  spi_pd(void)
       {
	        ISD_SS=0;
            isd1700_Npar_comm(ISD1700_PD,2);
			ISD_SS=1;
	   }

//读录音地址
void  spi_RD_rec_ptr(void)
       {    uchar i;
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_RD_REC_PTR,4);      
		    ISD_SS=1;
			i=ISD_COMM_RAM_C[3]&0x03;                 
            ISD_COMM_RAM_C[3]=ISD_COMM_RAM_C[2]; 
			ISD_COMM_RAM_C[2]=i;
			seril_back_sate(4);
	   }

//器件ID
void  spi_devid(void)
      {
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_DEVID,3);   
		    ISD_SS=1;
            ISD_COMM_RAM_C[2]=ISD_COMM_RAM_C[2]&0xf8; 
			seril_back_sate(3);
       }

//播放当前
void  spi_play(void)
       {   
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_PLAY|ISD_LED,2);    
		    ISD_SS=1;
         }


//开始录音
void  spi_rec (void)
       {
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_REC|ISD_LED,2);      
		    ISD_SS=1;
            ISD_COMM_RAM_C[0]=ISD1700_REC ;
            seril_back_sate(1);
	   }

//擦除当前
void  spi_erase (void)
       {
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_ERASE|ISD_LED,2);      
		    ISD_SS=1;
	   }

//擦除全部
void  spi_G_ERASE (void) 
       { 
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_G_ERASE|ISD_LED,2);    
		    ISD_SS=1;
	   }

//返回状态寄存器信息
void  spi_rd_apc(void)
      { 
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_RD_APC,4);
		    ISD_SS=1;
			seril_back_sate(4);
       }

//写寄存器
void  spi_wr_apc1 (void)
      {
      }
void  spi_wr_apc2 (void)
      {
             ISD_SS=0;
             isd1700_par2_comm(ISD1700_WR_APC2, 0x0400);
             ISD_SS=1;
      }

//将APC内容写入NVCFG
void  spi_wr_nvcfg (void)
      {
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_WR_NVCFG,2);      
		    ISD_SS=1;
	  }

////将NVCFG内容写入APC
void  spi_ld_nvcfg (void)
      {
	        ISD_SS=0;
            isd1700_Npar_comm(ISD1700_LD_NVCFG ,2);      
		    ISD_SS=1;
      }

//下一段
void  spi_fwd (void)
        {
            ISD_SS=0;
            isd1700_Npar_comm(ISD1700_FWD,2);      
		    ISD_SS=1;
		}

//检查环状存储体
void  spi_chk_mem(void)
        {
		    ISD_SS=0;
            isd1700_Npar_comm(ISD1700_CHK_MEM,2);      
		    ISD_SS=1;
		}

//状态返回
void  seril_back_sate(uchar byte_number)
        {
		  uchar sate_temp;
          rest_isd_comm_ptr();
		  sate_temp=0;
		  do{
		        SBUF=*back_data_ptr++;
		        while(!TI);
			    TI=0;
			 }while(++sate_temp<byte_number);
		}

//复位
void rest_isd_comm_ptr(void)
        {
	     isd_comm_ptr=ISD_COMM_RAM;
         back_data_ptr=ISD_COMM_RAM_C; 
        }

//无参数命令
void isd1700_Npar_comm (uchar comm_par,comm_byte_count)
       {   
	     uchar i;
		 i=0;
		 ISD_COMM_RAM[0]=comm_par;
		 isd_comm_ptr=&ISD_COMM_RAM[1];
		 do{ 
		     *isd_comm_ptr++=NULL;
		    }while(++i<comm_byte_count-1);

		 rest_isd_comm_ptr();		 
         i=0;
         do{
             *back_data_ptr++=T_R_comm_byte(*isd_comm_ptr++);
             i++;
           }while(i<comm_byte_count);
	    }
//2个字节命令
void isd1700_par2_comm(uchar comm_par, uint data_par)
        {
         uchar i;
	     ISD_COMM_RAM[0]=comm_par;
         ISD_COMM_RAM[1]=data_par;
         ISD_COMM_RAM[2]=data_par>>8;         
         rest_isd_comm_ptr();		 
         i=0;
         do{
             *back_data_ptr++=T_R_comm_byte(*isd_comm_ptr++);
             i++;
           }while(i<3);
        }

//7个字节命令
void isd1700_7byte_comm(uchar comm_par, uint star_addr, uint end_addr)
       {
         uchar i;
	     ISD_COMM_RAM[0]=comm_par;
 		 ISD_COMM_RAM[1]=NULL;
		 ISD_COMM_RAM[2]=star_addr;
         ISD_COMM_RAM[3]=star_addr>>8;
         ISD_COMM_RAM[4]=end_addr;
         ISD_COMM_RAM[5]=end_addr>>8;
         ISD_COMM_RAM[6]=NULL;
         rest_isd_comm_ptr();		 
         i=0;
         do{
             *back_data_ptr++=T_R_comm_byte(*isd_comm_ptr++);
             i++;
           }while(i<=7);
       }

//SPI接口函数
uchar T_R_comm_byte( uchar comm_data )
      {
         uchar bit_nuber;
		 uchar temp;
		 bit_nuber=0;
		 temp=0;
		 do{
		     ISD_SCLK=0;
             delay(1);
		     if((comm_data>>bit_nuber&0x01)!=0) 
                {ISD_MOSI=1;}
             else
			    {ISD_MOSI=0;}
             if(ISD_MISO)
			   {temp=(temp>>1)|0x80;}
             else
			   {temp=temp>>1;}
             ISD_SCLK=1;
             delay(1);
			 
			}while(++bit_nuber<=7);
         ISD_MOSI=0;
		  return (temp);
	  }

//短延时
static void delay(int x)
      {  
	     uchar i;
		 for(; x>=1; x--)
		  {for(;i<=20;i++);}
      }