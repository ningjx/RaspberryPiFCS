import serial
import time
import sys
import socket

ser = serial.Serial("/dev/ttyUSB0", 100000, timeout=0.5)
ser.parity = serial.PARITY_EVEN
ser.stopbits = 2
ser.bytesize = serial.EIGHTBITS

def main():
    server = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
    server.bind(('localhost',4664))
    server.listen(1)
    print("Start Listern")
    conn,addr = server.accept()
    print("Recive connection")
    while True:
        count = ser.inWaiting()
        if count != 0:
            recv = ser.read(count)
            #convert(recv)
            conn.send(recv)
        ser.flushInput()
        time.sleep(0.02)

def convert(bytes):
    print("----------------")
    for byte in bytes:
        print(byte)
    return 0

if __name__ == '__main__':
    try:
        main()
    except KeyboardInterrupt:
        if ser != None:
            ser.close()

