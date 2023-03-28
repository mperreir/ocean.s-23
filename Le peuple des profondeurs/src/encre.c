#include <FastLED.h>

/* Leds dependencies */
#define LED_PIN 8
#define NUM_LEDS 49  // Number of leds used
#define BRIGHTNESS 64
#define LED_TYPE WS2811
#define COLOR_ORDER RBG
#define UPDATES_PER_SECOND 120

CRGB leds[NUM_LEDS];

/* We define two CRGBPalette16 because there is a bug when we use the same */
CRGBPalette16 ocean_palette;
CRGBPalette16 ocean_palette2;
TBlendType currentBlending;

/* Buttons dependencies */
const int button_pin1 = 2;
const int button_pin2 = 3;
const int button_pin3 = 4;
const int button_pin4 = 5;
const int button_pin5 = 6;

/* Flags which indicate if a button has been pressed */
bool button1Pressed = false;
bool button1PressedDuringAnimation = false;
bool button2Pressed = false;
bool button3Pressed = false;
bool button4Pressed = false;
bool button5Pressed = true;

void setup() {
  /* leds logic setup */
  delay(3000);  // power-up safety delay
  FastLED.addLeds<LED_TYPE, LED_PIN, COLOR_ORDER>(leds, NUM_LEDS)
      .setCorrection(TypicalLEDStrip);
  FastLED.setBrightness(BRIGHTNESS);
  ocean_palette = ForestColors_p;
  ocean_palette2 = ForestColors_p;
  currentBlending = LINEARBLEND;

  /* button logic setup */
  /* The button_pin1 has been built with interruption */
  pinMode(button_pin1, INPUT_PULLUP);
  attachInterrupt(digitalPinToInterrupt(button_pin1), buttonInterrupt1,
                  FALLING);  // attach the interrupt to the falling edge of the
                             // signal on the button pin

  pinMode(button_pin2, INPUT_PULLUP);
  pinMode(button_pin3, INPUT_PULLUP);
  pinMode(button_pin4, INPUT_PULLUP);
  pinMode(button_pin5, INPUT_PULLUP);

  Serial.begin(9600);
}

void loop() {
  static uint8_t startIndex = 1;

  check_button_state();

  if (button1Pressed) {
    FastLED.clear();
    FillLEDsFromPaletteColorsDelay(startIndex, 1, 9);

    // Uncomment to make the leds static
    // FillLEDsFromPaletteColors(startIndex, 1, 4);
    // FillLEDsFromPaletteColorsDelay(startIndex, 6, 9);
    FastLED.show();
  } else if (button2Pressed) {
    FastLED.clear();
    FillLEDsFromPaletteColorsDelay(startIndex, 6, 15);
    // FillLEDsFromPaletteColors(startIndex, 6, 9);
    // FillLEDsFromPaletteColorsDelay(startIndex, 12, 15);
    FastLED.show();
    // button1PressedDuringAnimation = ;
  } else if (button3Pressed) {
    FastLED.clear();
    FillLEDsFromPaletteColorsDelay(startIndex, 12, 29);
    // FillLEDsFromPaletteColors(startIndex, 12, 15);
    // FillLEDsFromPaletteColorsDelay(startIndex, 24, 29);
    FastLED.show();
  } else if (button4Pressed) {
    FastLED.clear();
    FillLEDsFromPaletteColorsDelay(startIndex, 24, 56);
    // FillLEDsFromPaletteColors(startIndex, 24, 29);
    // FillLEDsFromPaletteColorsDelay(startIndex, 53, 56);
    FastLED.show();
  } else if (button5Pressed) {
    FastLED.clear();
    FillLEDsMainAnimation(startIndex);
    FastLED.show();
  }
}

/* Button functions */
/* Interruption function */
void buttonInterrupt1() {
  button1Pressed = true;
  Serial.println("1");
}

/* Check the button state */
void check_button_state() {
  /* Get the state of each button */
  int state2 = digitalRead(button_pin2);
  int state3 = digitalRead(button_pin3);
  int state4 = digitalRead(button_pin4);
  int state5 = digitalRead(button_pin5);

  if (button1PressedDuringAnimation) {
    button5Pressed = false;
  }

  if (state2 == HIGH) {
    Serial.println("2");  // Prints allow us to communicate with TouchDesigner
    button1Pressed = false;
    button1PressedDuringAnimation = false;
    button2Pressed = true;
  } else if (state3 == HIGH) {
    Serial.println("3");
    button2Pressed = false;
    button3Pressed = true;
  } else if (state4 == HIGH) {
    Serial.println("4");
    button3Pressed = false;
    button4Pressed = true;
  } else if (state5 == HIGH) {
    Serial.println("5");
    button4Pressed = false;
    button5Pressed = true;
  }
}

/* LEDS functions */
/* FillLEDsFromPaletteColorsDelay fills a range of LEDs with colors from a
color palette. */
void FillLEDsFromPaletteColors(uint8_t colorIndex, int indexLedMin,
                               int indexLedMax) {
  uint8_t brightness = 255;
  for (int i = indexLedMin; i <= indexLedMax; i++) {
    leds[i] = ColorFromPalette(ocean_palette, colorIndex, brightness,
                               currentBlending);
    colorIndex += 3;
  }
}

/* FillLEDsFromPaletteColorsDelay fills a range of LEDs with colors from a
color palette with a delay between each color being applied to the LEDs. */
void FillLEDsFromPaletteColorsDelay(uint8_t colorIndex, int indexLedMin,
                                    int indexLedMax) {
  uint8_t brightness = 255;
  for (int i = indexLedMin; i <= indexLedMax; i++) {
    leds[i] = ColorFromPalette(ocean_palette, colorIndex, brightness,
                               currentBlending);
    FastLED.delay(2500 / UPDATES_PER_SECOND);
    colorIndex += 3;
  }
}

/* This function sets the color of each LED in the LED strip, one at a time in a
reverse order, with a delay between each LED being set. This delay enable the
animation, which can be interrupted by the interruption with button1. */
void FillLEDsMainAnimation(uint8_t colorIndex) {
  uint8_t brightness = 255;
  for (int i = NUM_LEDS; i >= 0; i--) {
    if (button1Pressed) {
      button1PressedDuringAnimation = true;
      break;
    }
    leds[i] = ColorFromPalette(ocean_palette2, colorIndex, brightness,
                               currentBlending);
    FastLED.delay(1000 / UPDATES_PER_SECOND);
    colorIndex += 1;
  }
}