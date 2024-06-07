import cv2
from cvzone.PoseModule import PoseDetector
import socket


cap = cv2.VideoCapture(0)
cap.set(3, 1280)
cap.set(4, 720)

detector = PoseDetector()

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5058)

while True:
    success, img = cap.read()
    img = detector.findPose(img)
    lmList, bboxInfo = detector.findPosition(img)
    data=[]

    if bboxInfo:
        for lm in lmList:
            data.extend([lm[0], 720 - lm[1], lm[2]])

        #print(data)
        sock.sendto(str.encode(str(data)),serverAddressPort)
       
      
    img=cv2.resize(img,(0,0),None,1,1)    
    cv2.imshow("Image", img)
    cv2.waitKey(1)


