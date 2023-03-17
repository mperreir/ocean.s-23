#include <Seeed_CY8C401XX.h>
#include <ArduinoJson.h>
#include <ArduinoJson.hpp>

const int SLIDER_MAX_VALUE = 1024;

const int BUBBLE_B10K = A0;
CY8C hunter_launcher_sensor;

const int JOYSTICK_X_PIN = A1;
const int JOYSTICK_Y_PIN = A2;

const int START_END_BTN_PIN = 2;

DynamicJsonDocument json(1024);
bool valueUpdated = false;

void setup() {
  pinMode(START_END_BTN_PIN, INPUT);
  pinMode(BUBBLE_B10K, INPUT);
  pinMode(JOYSTICK_X_PIN, INPUT);
  pinMode(JOYSTICK_Y_PIN, INPUT);
  pinMode(LED_BUILTIN, OUTPUT);

  hunter_launcher_sensor.init();

  Serial.begin(115200);
}


int sliderFormat(const int value, const int steps) {
  return value / (SLIDER_MAX_VALUE / steps);
}

int joystickFormat(const int value) {
  if (value < 400) return -1;
  if (value > 700) return 1;
  return 0;
}

void updateValue(const String name, const int value){
  if (json[name] == value) return;
  json[name] = value;
  valueUpdated = true;
}

bool btn1Pressed = false;
bool sliderPressed[5] = {false, false, false, false, false};
bool btn2Pressed = false;

void resetLaunchHunterRoutine() {
  btn1Pressed = false;
  for (int i = 0; i < 5; i++) sliderPressed[i] = false;
  btn2Pressed = false;
}

bool lauchHunter(){
  u8 btnValue = 0, sliderValue = 0;
  hunter_launcher_sensor.get_touch_button_value(&btnValue);
  // btn 1 pressed

  if (btnValue & 0x01) btn1Pressed = true;
  if (!btn1Pressed) return false;

  hunter_launcher_sensor.get_touch_slider_value(&sliderValue);

  if (sliderValue <= 100 && sliderValue >= 80) sliderPressed[0] = true;
  if (!sliderPressed[0]) return false;
  
  if (sliderValue <= 80 && sliderValue >= 60) sliderPressed[1] = true;
  if (!sliderPressed[1]) return false;

  if (sliderValue <= 60 && sliderValue >= 40) sliderPressed[2] = true;
  if (!sliderPressed[2]) return false;

  if (sliderValue <= 40 && sliderValue >= 20) sliderPressed[3] = true;
  if (!sliderPressed[3]) return false;

  if (sliderValue <= 20 && sliderValue >= 0) sliderPressed[4] = true;
  if (!sliderPressed[4]) return false;

  // btn 2 pressed
  if (btnValue & 0x2) btn2Pressed = true;
  if (!btn2Pressed) return false;

  resetLaunchHunterRoutine();
  return true;
}

bool connected = false;

int nb_conn = 0;

void loop() {
  // 0 mean down/pressed
  updateValue("StartEndButton", digitalRead(START_END_BTN_PIN) == 1);
  updateValue("BubbleStream", sliderFormat(analogRead(BUBBLE_B10K), 10));
  updateValue("JoystickX", joystickFormat(analogRead(JOYSTICK_X_PIN)));
  updateValue("JoystickY", joystickFormat(analogRead(JOYSTICK_Y_PIN)));
  updateValue("LaunchHunter", lauchHunter());


  serializeJson(json, Serial);
  Serial.println();

  delay(16);
}
