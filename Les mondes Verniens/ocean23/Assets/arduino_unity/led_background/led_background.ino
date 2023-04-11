#include <FastLED.h>
#define NUM_LEDS 60
CRGB leds[NUM_LEDS];

void setup() { 
  FastLED.addLeds<NEOPIXEL, 2>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 3>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 4>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 5>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 6>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 7>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 8>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 9>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 10>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 11>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 12>(leds, NUM_LEDS); 
  FastLED.addLeds<NEOPIXEL, 13>(leds, NUM_LEDS); 
  leds[0] = CRGB::Blue; FastLED.show();
}

void loop() {
  for (int i = 0; i < NUM_LEDS; i++) leds[i] = CRGB::Blue;
  FastLED.show();
}