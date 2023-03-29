int buttonPin = 4;
int fanPin = 2;

int buttonState;

void setup() {
  Serial.begin(9600);
  pinMode(buttonPin, INPUT);
  pinMode(fanPin, OUTPUT);


}

void loop() 
{
  // Checks if the button state has change
  buttonState = digitalRead(buttonPin);
  Serial.println(buttonState);
  if (buttonState == LOW) 
  {
   // Turns the fan on    
    digitalWrite(fanPin, HIGH);  
  } 
  else 
  {
    // Turns the fan off
    digitalWrite(fanPin, LOW); 
  }
}

