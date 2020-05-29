import serial
cdef public InitPort(portName,baudRate,parity,databits,stopBits):
    global ser
    ser = serial.Serial(portName,baudRate)
    ser.bytesize = databits
    ser.parity = parity
    ser.stopbits = stopBits
    ser.timeout = 0.5
    ser.writeTimeout = 0.5
    ser.open()
 
cdef public Read():
    return (bytes)ser.read()

cdef public Write(bytes):
    ser.write(bytes)

cdef public Close():
    ser.close()