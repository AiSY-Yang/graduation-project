#include "stm32f10x.h"
#include "lcd.h"
#include "stdio.h"
#include "i2c.h"
#include "init.h"
#include "stdbool.h"
//全局延时变量
u32 TimingDelay = 0;

double adv=0;
int16_t height=0;
char ch[20]={0};

//state
bool warring=false;
uint16_t ledstate=0xffff;
bool displaystate=false;

void send(char* str);
uint8_t key(void);
void ledclose(void);
int main(void)
{
  GPIO();
	STM3210B_LCD_Init();
	LCD_Clear(White);
	LCD_SetTextColor(Black);
	LCD_SetBackColor(White);
	ADC();
	USART();
	RTCinit();
	//界面初始化
	ADC_SoftwareStartConvCmd(ADC1, ENABLE);
	adv=ADC_GetConversionValue(ADC1)*3.3/0xfff;
	height=adv*100/3.3;
	LCD_DisplayStringLine(Line0,(u8*)"    Liquid Height");
	sprintf(ch,"Height=%-3d",height);
	LCD_DisplayStringLine(Line1,(u8*)ch);
	sprintf(ch,"ADC:%1.2fV",adv);
	LCD_DisplayStringLine(Line2,(u8*)ch);
	sprintf(ch,"H%d\n",height);
	send("HENU");
	send("Boot");
	SysTick_Config(SystemCoreClock/1000);
	while(1)
	{
		switch(key())
		{
			case 1:
			{
				if(!displaystate)
				{
					send("Time");
					LCD_ClearLine(Line7);
					LCD_DisplayStringLine(Line7,(u8*)"Time Calibration");
				}
				break;
			}
			case 2:
			{
				if(!displaystate)
				{
					send("Alarm");
					LCD_ClearLine(Line7);
					LCD_DisplayStringLine(Line7,(u8*)"Loopback test");
				}
				break;
			}
			case 3:
			{
				warring=false;
				ledclose();
				send("Lift alarm");
				LCD_ClearLine(Line7);
				LCD_DisplayStringLine(Line7,(u8*)"Lift alarm");
				break;
			}
			case 4:
			{
				break;			
			}

		}
	}//while1
}//main


	void Delay_Ms(u32 nTime)
{
	TimingDelay = nTime;
	while(TimingDelay != 0);	
}
#define K1 GPIO_ReadInputDataBit(GPIOA,GPIO_Pin_0)
#define K2 GPIO_ReadInputDataBit(GPIOA,GPIO_Pin_8)
#define K3 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_1)
#define K4 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_2)
uint8_t key(void)
{
	uint8_t i;
	if(K1==0) {i=1; while(!K1);}
	if(K2==0) {i=2; while(!K2);}
	if(K3==0) {i=3; while(!K3);}
	if(K4==0) {i=4; while(!K4);}
	return i;
}

void send(char* str)
{
	 int i=0;
	 do
	 {
		 USART_SendData(USARTz,str[i]);
		 while(USART_GetFlagStatus(USARTz, USART_FLAG_TC)== RESET);
		 i++;
	 }
	while(str[i]!=0);
	//'\n'
	USART_SendData(USARTz,10);
	while(USART_GetFlagStatus(USARTz, USART_FLAG_TC)== RESET);
}
void ledclose(void)
{
	ledstate=0xffff;
	GPIOC->BSRR=0xff<<8;
	GPIOD->BSRR=1<<2;
	GPIOD->BRR=1<<2;
}

uint8_t eepred(uint8_t add)
{
	int i;
	I2CStart();
	I2CSendByte(0xA0);
	I2CWaitAck();
	
	
	I2CSendByte(add);
	I2CWaitAck();
	
	I2CStart();
	I2CSendByte(0xA1);
	I2CWaitAck();
	
	
	i=I2CReceiveByte();
	I2CWaitAck();
	I2CStop();
	return i;
}

void eepwri(uint8_t add,uint8_t byte)
{
	I2CStart();
	I2CSendByte(0xA0);
	I2CWaitAck();
	
	
	I2CSendByte(add);
	I2CWaitAck();
	
	I2CSendByte(byte);
	I2CWaitAck();
	I2CStop();
}


