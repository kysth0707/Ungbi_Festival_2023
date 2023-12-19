/*
 * 
 * 무한의 계단
 * 
 * 발, 턴 인식 부분
 * 
 */
 
#include <Adafruit_NeoPixel.h>

//#define LEFT_TRIG_PIN 2
//#define LEFT_ECHO_PIN 3
//#define LEFT_OFFSET 20
//
//#define RIGHT_TRIG_PIN 4
//#define RIGHT_ECHO_PIN 5
//#define RIGHT_OFFSET 20

#define LEFT_FOOT_PIN 2
#define RIGHT_FOOT_PIN 3

#define TOP_DOUBLE_BUTTON_PIN 6
#define VIBRATION 8

#define LED_PIN 9
#define LED_COUNT 60

Adafruit_NeoPixel strip(LED_COUNT, LED_PIN, NEO_GRB + NEO_KHZ800);

int leftLEDTime = 0;
int rightLEDTime = 0;
int turnLEDTime = 0;
bool leftLEDFlag = false;
bool rightLEDFlag = false;
bool turnLEDFlag = false;

int isLeftFirst = 0; //최초 클릭만 감지
int isRightFirst = 0;
int isButtonFirst = 0;

void setup()
{
//    pinMode(LEFT_TRIG_PIN, OUTPUT);
//    pinMode(LEFT_ECHO_PIN, INPUT);
//    pinMode(RIGHT_TRIG_PIN, OUTPUT);
//    pinMode(RIGHT_ECHO_PIN, INPUT);

    pinMode(TOP_DOUBLE_BUTTON_PIN, INPUT);
    pinMode(VIBRATION, OUTPUT);
    pinMode(LEFT_FOOT_PIN, INPUT);
    pinMode(RIGHT_FOOT_PIN, INPUT);

    strip.begin();
    strip.clear();
    strip.show();
    strip.setBrightness(50);

    Serial.begin(115200);
}

void loop()
{
    int isLeftPressed = digitalRead(LEFT_FOOT_PIN);;
    delay(10);
    int isRightPressed = digitalRead(RIGHT_FOOT_PIN);
    int isButtonPressed = !digitalRead(TOP_DOUBLE_BUTTON_PIN);
//    버튼 누르면 진동으로 사용자에게 피드백
    digitalWrite(VIBRATION, isButtonPressed);
    
    if(isLeftPressed) {
        if(!leftLEDFlag)
        {
            leftLEDTime = 0;
            leftLEDFlag = true;
            isLeftFirst = 1;
        }
    } else { leftLEDFlag = false; }
    if(isRightPressed) {
        if(!rightLEDFlag)
        {
            rightLEDTime = 0;
            rightLEDFlag = true;
            isRightFirst = 1;
        }
    } else { rightLEDFlag = false; }
    if(isButtonPressed) {
        if(!turnLEDFlag)
        {
            turnLEDTime = 0;
            turnLEDFlag = true;
            isButtonFirst = 1;
        }
    } else { turnLEDFlag = false; }

    if(leftLEDTime < 10)
    {
        leftLEDTime += 1;
        ledOnByVariable(true, leftLEDTime);
    }
    if(rightLEDTime < 10)
    {
        rightLEDTime += 1;
        ledOnByVariable(false, rightLEDTime);
    }
    if(turnLEDTime < 10)
    {
        turnLEDTime += 1;
        ledTurn(turnLEDTime);
    }else{strip.clear();}
    
//    sprintf 를 이용하여 문자열 합치고 Serial.println    
    char buff[100];
    sprintf(buff, "%d %d %d", isButtonFirst, isLeftFirst, isRightFirst);
    Serial.println(buff);
    isButtonFirst = 0;
    isLeftFirst = 0;
    isRightFirst = 0;
    delay(40);

}

void ledOnByVariable(bool isLeft, int timeValue)
{
    int halfNum = strip.numPixels() / 2;

    if(isLeft)
    {
        for(int i = 0; i < halfNum; i++)
        {
            strip.setPixelColor(i, strip.Color((255 / 10) * (10 - timeValue), 0, 0));
        }
    }
    else
    {
        for(int i = halfNum; i < strip.numPixels(); i++)
        {
            strip.setPixelColor(i, strip.Color((255 / 10) * (10 - timeValue), 0, 0));
        }
    }
    strip.show();
}

void ledTurn(int timeValue)
{
    int halfNum = strip.numPixels() / 2;
    for(int i = 0; i < 10; i++)
    {
        strip.setPixelColor(halfNum - (timeValue-1)*4 + i, strip.Color(0, 0, 0));
        strip.setPixelColor(halfNum + (timeValue-1)*4 - i, strip.Color(0, 0, 0));
        
        strip.setPixelColor(halfNum - timeValue*4 + i, strip.Color(0, 255, 0));
        strip.setPixelColor(halfNum + timeValue*4 - i, strip.Color(0, 255, 0));
    }
    strip.show();
}

// 버튼 두 개에 대해 or 연산 (그냥 편하게 함수로)
//int isHandButtonPressed(int buttonPin)
//{
//    int val = digitalRead(TOP_DOUBLE_BUTTON_PIN);
//    if(val)
//    {
//        return 0;
//    }
//    else
//    {
//        return 1;
//    }
//}

// 발이 감지되었는지
//int isFootDown(int trigPin, int echoPin, int offset)
//{
//    if (checkDistance(trigPin, echoPin) < offset)
//    {
//        return 1;
//    }
//    else
//    {
//        return 0;
//    }
//}

// 초음파 센서로 거리 감지
//long checkDistance(int trigPin, int echoPin)
//{
//    long distance, durationMS;
//    
//    digitalWrite(trigPin, LOW);
//    delayMicroseconds(10);
//    digitalWrite(trigPin, HIGH);
//    delayMicroseconds(10);
//    digitalWrite(trigPin, LOW);
//
//    durationMS = pulseIn(echoPin, HIGH, 30000);
//    distance = ((float)(340 * durationMS) / 10000) / 2;
//
//    return distance;
//}

