"""

A S D
Z X C

로 오프셋 설정

"""

import cv2 as cv
import copy

cascadePath = r'E:\GithubProjects\Ungbi_Festival_2023\SubwaySurfer\python\haarcascade_frontalface_default.xml'
faceCascade = cv.CascadeClassifier(cascadePath)


RED = (255, 0, 0)
Offsets = {
	"standingY" : 0,
	"crouchY" : 0,
	"leftX" : 0,
	"centerX" : 0,
	"rightX" : 0,
}

cap = cv.VideoCapture(0)
_, img = cap.read()
height, width = img.shape[:2]
while True:
	ret, img = cap.read()
	if not ret:
		print("error")
		continue
	showImage = copy.deepcopy(img)
	gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)

	faces = faceCascade.detectMultiScale(gray, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))

	biggestHead = 0
	biggestData = None
	for (x, y, w, h) in faces:
		cv.rectangle(showImage, (x, y), (x+w, y+h), (0, 255, 0), 3)
		if w * h > biggestHead:
			biggestHead = w * h
			biggestData = (x, y, w, h)

	currentKey = cv.waitKey(1)
	if biggestData != None:
		if currentKey == ord('z'):
			Offsets['leftX'] = x
			Offsets['crouchY'] = y
		elif currentKey == ord('x'):
			Offsets['centerX'] = x
			Offsets['crouchY'] = y
		elif currentKey == ord('c'):
			Offsets['rightX'] = x
			Offsets['crouchY'] = y

		elif currentKey == ord('a'):
			Offsets['leftX'] = x
			Offsets['standingY'] = y
		elif currentKey == ord('s'):
			Offsets['centerX'] = x
			Offsets['standingY'] = y
		elif currentKey == ord('d'):
			Offsets['rightX'] = x
			Offsets['standingY'] = y



		cv.line(showImage, (Offsets['leftX'], 0), (Offsets['leftX'],height), RED, 5)
		cv.line(showImage, (Offsets['rightX'], 0), (Offsets['rightX'],height), RED, 5)
		cv.line(showImage, (Offsets['centerX'], 0), (Offsets['centerX'],height), RED, 5)
		
		cv.line(showImage, (0, Offsets['standingY']), (width, Offsets['standingY']), RED, 5)
		cv.line(showImage, (0, Offsets['crouchY']), (width, Offsets['crouchY']), RED, 5)

		x, y, _, _ = biggestData
		

	cv.imshow("show", showImage)

	if currentKey == ord('q'):
		break

cap.release()
cv.destroyAllWindows()