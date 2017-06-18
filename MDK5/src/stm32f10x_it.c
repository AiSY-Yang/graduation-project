/**
  ******************************************************************************
  * @file    I2S/SPI_I2S_Switch/stm32f10x_it.c
  * @author  MCD Application Team
  * @version V3.5.0
  * @date    08-April-2011
  * @brief   Main Interrupt Service Routines.
  *          This file provides template for all exceptions handler and peripherals
  *          interrupt service routine.
  ******************************************************************************
  * @attention
  *
  * THE PRESENT FIRMWARE WHICH IS FOR GUIDANCE ONLY AIMS AT PROVIDING CUSTOMERS
  * WITH CODING INFORMATION REGARDING THEIR PRODUCTS IN ORDER FOR THEM TO SAVE
  * TIME. AS A RESULT, STMICROELECTRONICS SHALL NOT BE HELD LIABLE FOR ANY
  * DIRECT, INDIRECT OR CONSEQUENTIAL DAMAGES WITH RESPECT TO ANY CLAIMS ARISING
  * FROM THE CONTENT OF SUCH FIRMWARE AND/OR THE USE MADE BY CUSTOMERS OF THE
  * CODING INFORMATION CONTAINED HEREIN IN CONNECTION WITH THEIR PRODUCTS.
  *
  * <h2><center>&copy; COPYRIGHT 2011 STMicroelectronics</center></h2>
  ******************************************************************************
  */

/* Includes ------------------------------------------------------------------*/


/** @addtogroup STM32F10x_StdPeriph_Examples
  * @{
  */

/** @addtogroup I2S_SPI_I2S_Switch
  * @{
  */

/* Private typedef -----------------------------------------------------------*/
/* Private define ------------------------------------------------------------*/
/* Private macro -------------------------------------------------------------*/
/* Private variables ---------------------------------------------------------*/
/* Private function prototypes -----------------------------------------------*/
/* Private functions ---------------------------------------------------------*/

/******************************************************************************/
/*            Cortex-M3 Processor Exceptions Handlers                         */
/******************************************************************************/

/**
  * @brief  This function handles NMI exception.
  * @param  None
  * @retval None
  */
void NMI_Handler(void)
{
}

/**
  * @brief  This function handles Hard Fault exception.
  * @param  None
  * @retval None
  */
void HardFault_Handler(void)
{
	/* Go to infinite loop when Hard Fault exception occurs */
	while (1)
		{}
}

/**
  * @brief  This function handles Memory Manage exception.
  * @param  None
  * @retval None
  */
void MemManage_Handler(void)
{
	/* Go to infinite loop when Memory Manage exception occurs */
	while (1)
		{}
}

/**
  * @brief  This function handles Bus Fault exception.
  * @param  None
  * @retval None
  */
void BusFault_Handler(void)
{
	/* Go to infinite loop when Bus Fault exception occurs */
	while (1)
		{}
}

/**
  * @brief  This function handles Usage Fault exception.
  * @param  None
  * @retval None
  */
void UsageFault_Handler(void)
{
	/* Go to infinite loop when Usage Fault exception occurs */
	while (1)
		{}
}

/**
  * @brief  This function handles Debug Monitor exception.
  * @param  None
  * @retval None
  */
void DebugMon_Handler(void)
{
}

/**
  * @brief  This function handles SVCall exception.
  * @param  None
  * @retval None
  */
void SVC_Handler(void)
{
}

/**
  * @brief  This function handles PendSV_Handler exception.
  * @param  None
  * @retval None
  */
void PendSV_Handler(void)
{
}

/**
  * @brief  This function handles SysTick Handler.
  * @param  None
  * @retval None
  */
#include <stm32f10x.h>
#include "stm32f10x_it.h"
#include "stdio.h"
#include "lcd.h"
#include "stdbool.h"
#include "init.h"

#define USARTz                   USART2
#define USARTz_GPIO              GPIOA
#define USARTz_CLK               RCC_APB1Periph_USART2
#define USARTz_GPIO_CLK          RCC_APB2Periph_GPIOA
#define USARTz_RxPin             GPIO_Pin_3
#define USARTz_TxPin             GPIO_Pin_2
#define USARTz_IRQn              USART2_IRQn
#define USARTz_IRQHandler        USART2_IRQHandler

extern double adv;
extern int16_t height;
extern u32 TimingDelay;
extern bool warring;
extern char ch[];
extern uint16_t ledstate;
extern bool displaystate;

NVIC_InitTypeDef NVIC_RTC_InitStructure;
uint16_t ms=0;
uint16_t link=0;
uint8_t itstate;

//usart
unsigned char USARTbuff[30]= {0};
uint16_t USARTcount=0;
//time
uint16_t yyyy=0;
uint16_t MM=0;
uint16_t dd=0;
uint16_t hh=0;
uint16_t mm=0;
uint32_t ss=0;
char timedisplaybuff[20]= {0};

void ledclose(void);
void send(char* str);

void SysTick_Handler(void)
{
	ms++;
	if(ms%10==0)
	{
		ADC_SoftwareStartConvCmd(ADC1, ENABLE);
		adv=ADC_GetConversionValue(ADC1)*3.3/0xfff;
		height=adv*100/3.3;
		if(!displaystate)
		{
			sprintf(ch,"Height=%-3d",height);
			LCD_DisplayStringLine(Line1,(u8*)ch);
			sprintf(ch,"ADC:%1.2fV",adv);
			LCD_DisplayStringLine(Line2,(u8*)ch);
		}
		sprintf(ch,"H%d",height);
		send(ch);
	}
	if(ms%500==0)
	{
		link++;
		if(warring==true)
		{
			ledstate^=256;
			GPIOC->BSRR=ledstate;
			GPIOD->BSRR=1<<2;
			GPIOD->BRR=1<<2;
		}
		if(link>10)
		{
			LCD_ClearLine(Line7);
			LCD_DisplayStringLine(Line7,(u8*)"No link");
			warring=true;
			GPIO_ResetBits(GPIOB,GPIO_Pin_4);
		}
	}
}

void USARTz_IRQHandler(void)
{
	if(USART_GetITStatus(USARTz, USART_IT_RXNE) != RESET)
	{
		USART_ClearITPendingBit(USARTz, USART_IT_RXNE);
		USARTbuff[USARTcount]=USART_ReceiveData(USARTz);
		if(USARTbuff[USARTcount]==10)
		{
			USARTcount=0;
			//Time
			if(USARTbuff[0]=='T')
			{
				USART_ITConfig(USART2,USART_IT_RXNE,DISABLE);
				//string to int
				yyyy=(USARTbuff[1]-'0')*1000+(USARTbuff[2]-'0')*100+(USARTbuff[3]-'0')*10+USARTbuff[4]-'0';
				MM=(USARTbuff[5]-'0')*10+(USARTbuff[6]-'0');
				dd=(USARTbuff[7]-'0')*10+(USARTbuff[8]-'0');
				hh=(USARTbuff[10]-'0')*10+(USARTbuff[11]-'0');
				mm=(USARTbuff[12]-'0')*10+(USARTbuff[13]-'0');
				ss=(USARTbuff[14]-'0')*10+(USARTbuff[15]-'0');
				//Display
				sprintf(timedisplaybuff,"%d-%d-%d %2d:%2d:%2d",yyyy,MM,dd,hh,mm,ss);
				LCD_DisplayStringLine(Line6,(u8*)timedisplaybuff);
				//enable rtc
				RTC_SetCounter(hh*3600+mm*60+ss);
				RTC_WaitForLastTask();
				//Enable the RTC Interrupt
				NVIC_RTC_InitStructure.NVIC_IRQChannel = RTC_IRQn;
				NVIC_RTC_InitStructure.NVIC_IRQChannelPreemptionPriority = 1;
				NVIC_RTC_InitStructure.NVIC_IRQChannelSubPriority = 0;
				NVIC_RTC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
				NVIC_Init(&NVIC_RTC_InitStructure);
				USART_ITConfig(USART2,USART_IT_RXNE,ENABLE);
			}
			//From point to graph
			if(USARTbuff[0]=='P')
			{
				if(USARTbuff[1]=='c')//start
				{
					LCD_ClearLine(Line9);
					displaystate=true;
					LCD_ClearLine(Line7);
					LCD_DisplayStringLine(Line7,(u8*)"Wait....");
					return;
				}
				if(USARTbuff[1]=='o')//stop
				{
					displaystate=false;
					return;
				}
				//ctrl
				if(USARTbuff[1]=='y')
				{
					LCD_SetCursor(215+(int)USARTbuff[2],0);
					LCD_WriteRAM_Prepare();
					return;
				}
				if(USARTbuff[1]=='1')
				{
					LCD_WriteRAM(Red);
				}
				if(USARTbuff[1]=='0')
				{
					LCD_WriteRAM(White);
				}
			}
			//Alarm
			if(USARTbuff[0]=='A')
			{
				if(USARTbuff[2]=='n')
				{
					warring=true;
					GPIO_ResetBits(GPIOB,GPIO_Pin_4);
				}
				else
				{
					warring=false;
					ledclose();
					GPIO_SetBits(GPIOB,GPIO_Pin_4);
				}
			}
			//Link
			if(USARTbuff[0]=='L')
			{
				if(link>10)
				{
					warring=false;
					ledclose();
				}
				link=0;
				LCD_ClearLine(Line7);
				LCD_DisplayStringLine(Line7,(u8*)"Link");
			}
		}
		else
		{
			USARTcount++;
		}
	}
}

void RTC_IRQHandler(void)
{
	if (RTC_GetITStatus(RTC_IT_SEC) != RESET)
	{
		RTC_ClearITPendingBit(RTC_IT_SEC);
		RTC_WaitForLastTask();
		ss=RTC_GetCounter();

		/* 23:59:59 */
		if (ss >= 0x00015180)
		{
			RTC_SetCounter(0x0);
			RTC_WaitForLastTask();
			dd++;
		}
		hh = ss / 3600;
		mm = (ss % 3600) / 60;
		ss = (ss % 3600) % 60;
		if(!displaystate)
		{
			LCD_ClearLine(Line6);
			sprintf(timedisplaybuff,"%d-%d-%d %2d:%2d:%2d",yyyy,MM,dd,hh,mm,ss);
			LCD_DisplayStringLine(Line6,(u8*)timedisplaybuff);
		}
	}
}
#define K1 GPIO_ReadInputDataBit(GPIOA,GPIO_Pin_0)
#define K2 GPIO_ReadInputDataBit(GPIOA,GPIO_Pin_8)
#define K3 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_1)
#define K4 GPIO_ReadInputDataBit(GPIOB,GPIO_Pin_2)
//A0 button1
//时间校准
void EXTI0_IRQHandler(void)
{
	itstate++;
	if(EXTI_GetITStatus(EXTI_Line0) != RESET)
	{
		if(!displaystate)
		{
			send("Time");
			LCD_ClearLine(Line7);
			LCD_DisplayStringLine(Line7,(u8*)"Time Calibration");
		}
		EXTI_ClearITPendingBit(EXTI_Line0);
	}
}

//A8 button2
//紧急报警
void EXTI9_5_IRQHandler(void)
{
	itstate++;
	if(EXTI_GetITStatus(EXTI_Line8) != RESET)
	{
		if(!displaystate)
		{
			send("Alarm");
			LCD_ClearLine(Line7);
			LCD_DisplayStringLine(Line7,(u8*)"Loopback test");
		}
		EXTI_ClearITPendingBit(EXTI_Line8);
	}
}
/******************************************************************************/
/*                 STM32F10x Peripherals Interrupt Handlers                   */
/*  Add here the Interrupt Handler for the used peripheral(s) (PPP), for the  */
/*  available peripheral interrupt handler's name please refer to the startup */
/*  file (startup_stm32f10x_xx.s).                                            */
/******************************************************************************/

/**
  * @brief  This function handles PPP interrupt request.
  * @param  None
  * @retval None
  */
/*void PPP_Switch_IRQHandler(void)
{
}*/

/**
  * @}
  */

/**
  * @}
  */

/******************* (C) COPYRIGHT 2011 STMicroelectronics *****END OF FILE****/
