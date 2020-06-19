import serial
class MySerial:
    def InitPort(portName,baudRate,parity,databits,stopBits):
        global ser
        ser = serial.Serial(portName,baudRate)

        ser.bytesize = databits
        if parity == 0:
            ser.parity = serial.PARITY_NONE
        elif parity == 1:
            ser.parity = serial.PARITY_ODD
        elif parity == 2:
            ser.parity = serial.PARITY_EVEN
        elif parity == 3:
            ser.parity = serial.PARITY_MARK
        elif parity == 4:
            ser.parity = serial.PARITY_SPACE

        ser.stopbits = stopBits
        ser.timeout = 0.5
        ser.writeTimeout = 0.5
 
    def Read():
        count = ser.inWaiting()
        recv = ser.read(count)
        ser.flushInput()
        return recv

    def Write(bytes):
        ser.write(bytes)

    def Close():
        ser.close()