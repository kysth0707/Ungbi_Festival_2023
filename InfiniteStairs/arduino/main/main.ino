/*
 * 
 * 무한의 계단
 * 
 * 발, 턴 인식 부분
 * 
 */

#define LEFT_TRIG_PIN 2
#define LEFT_ECHO_PIN 3
#define LEFT_OFFSET 12

#define RIGHT_TRIG_PIN 4
#define RIGHT_ECHO_PIN 5
#define RIGHT_OFFSET 12

#define TOP_BUTTON_PIN 6
#define BOTTOM_BUTTON_PIN 7
#define VIBRATION 8

#define HIGH_PIN 9

void setup()
{
    pinMode(LEFT_TRIG_PIN, OUTPUT);
    pinMode(LEFT_ECHO_PIN, INPUT);
    pinMode(RIGHT_TRIG_PIN, OUTPUT);
    pinMode(RIGHT_ECHO_PIN, INPUT);

    pinMode(TOP_BUTTON_PIN, INPUT);
    pinMode(BOTTOM_BUTTON_PIN, INPUT);
    pinMode(VIBRATION, OUTPUT);
    pinMode(HIGH_PIN, OUTPUT);
    digitalWrite(HIGH_PIN, HIGH);

    Serial.begin(9600);
}

void loop()
{
    /*
    int isLeftPressed = isFootDown(LEFT_TRIG_PIN, LEFT_ECHO_PIN, LEFT_OFFSET);
    delay(10);
    int isRightPressed = isFootDown(RIGHT_TRIG_PIN, RIGHT_ECHO_PIN, RIGHT_OFFSET);
    */

//    int isButtonPressed = isHandButtonPressed(TOP_BUTTON_PIN, BOTTOM_BUTTON_PIN);
//    버튼 누르면 진동으로 사용자에게 피드백
//    digitalWrite(VIBRATION, isButtonPressed);
    Serial.print(digitalRead(6));
    Serial.println(digitalRead(7));
    
    /*
//    sprintf 를 이용하여 문자열 합치고 Serial.println    
    char buff[100];
    sprintf(buff, "%d %d %d", isButtonPressed, isLeftPressed, isRightPressed);
    Serial.println(buff);
    */
    delay(40);
}

// 버튼 두 개에 대해 or 연산 (그냥 편하게 함수로)
//int isHandButtonPressed(int button1Pin, int button2Pin)
//{
//    int val1 = digitalRead(button1Pin);
//    int val2 = digitalRead(button2Pin);
//    Serial.print(val1);
//    Serial.println(val2);
//
//    if(val1 || val2)
//    {
//        return 1;
//    }
//    else
//    {
//        return 0;
//    }
//}

// 발이 감지되었는지
int isFootDown(int trigPin, int echoPin, int offset)
{
    if (checkDistance(trigPin, echoPin) < offset)
    {
        return 1;
    }
    else
    {
        return 0;
    }
}

// 초음파 센서로 거리 감지
long checkDistance(int trigPin, int echoPin)
{
    long distance, durationMS;
    
    digitalWrite(trigPin, LOW);
    delayMicroseconds(10);
    digitalWrite(trigPin, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPin, LOW);

    durationMS = pulseIn(echoPin, HIGH);
    distance = ((float)(340 * durationMS) / 10000) / 2;

    return distance;
}

