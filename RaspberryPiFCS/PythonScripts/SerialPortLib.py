import serial #导入模块
def InitPort(portName,baudRate,parity,databits,stopBits):
   #超时设置,None：永远等待操作，0为立即返回请求结果，其他值为等待超时时间(单位为秒)
   # 打开串口，并得到串口对象
    global ser
    ser = serial.Serial(portName,baudRate)
    ser.bytesize = databits#字节大小
    ser.parity = parity
    ser.stopbits = stopBits#停止位
    ser.timeout = 0.5#读超时设置
    ser.writeTimeout = 0.5#写超时
    ser.open()
 
def Read():
    return ser.read()

def Write(bytes):
    ser.write(bytes)

def Close():
    ser.close()#关闭串口