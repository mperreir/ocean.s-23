#define X_PIN A0
#define Y_PIN A1

int xValue = 0;
int yValue = 0;
void setup() {
  Serial.begin(9600);

}

void loop() {
  xValue = analogRead(X_PIN);
  yValue = analogRead(Y_PIN);

  Serial.print(xValue);
  Serial.print(",");
  Serial.println(yValue);
  delay(200);

}
