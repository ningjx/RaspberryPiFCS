import serial
class MySerial:
    def InitPort(portName,baudRate,parity,databits,stopBits):
        global ser
        ser = serial.Serial(portName,baudRate)
        ser.bytesize = databits
        ser.parity = parity
        ser.stopbits = stopBits
        ser.timeout = 0.5
        ser.writeTimeout = 0.5
        ser.open()
 
    def Read():
        return ser.read()

    def Write(bytes):
        ser.write(bytes)

    def Close():
        ser.close()