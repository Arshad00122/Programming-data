import cv2
import face_recognition
import numpy as np
import os
from datetime import datetime

# Initialize variables
saved_face_encoding = None
saved_face_image = None
face_saved = False

# Start video capture
cap = cv2.VideoCapture(0)
if not cap.isOpened():
    print("Error: Could not open camera.")
    exit()

# Load Haar cascade for face detection
face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')
if face_cascade.empty():
    print("Error: Could not load Haar cascade.")
    exit()

def save_face_image(frame, face_location):
    """Save the detected face image to a file."""
    global saved_face_image
    top, right, bottom, left = face_location
    face_image = frame[top:bottom, left:right]
    timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
    filename = f"saved_face_{timestamp}.jpg"
    cv2.imwrite(filename, face_image)
    print(f"Face saved as {filename}")
    return face_image

def get_face_encoding(image):
    """Get face encoding from an image."""
    rgb_image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    encodings = face_recognition.face_encodings(rgb_image)
    return encodings[0] if encodings else None

while True:
    ret, frame = cap.read()
    if not ret:
        print("Error: Could not read frame.")
        break

    # Convert to grayscale for face detection
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
    
    # Detect faces
    faces = face_cascade.detectMultiScale(gray, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))
    
    for (x, y, w, h) in faces:
        # Draw rectangle around detected face
        cv2.rectangle(frame, (x, y), (x+w, y+h), (0, 255, 0), 2)
        
        if not face_saved:
            # Prompt to save the first face
            cv2.putText(frame, "Press 's' to save this face", (x, y-10), 
                       cv2.FONT_HERSHEY_SIMPLEX, 0.9, (0, 255, 0), 2)
        else:
            # Compare detected face with saved face
            face_image = frame[y:y+h, x:x+w]
            current_encoding = get_face_encoding(face_image)
            
            if current_encoding is not None and saved_face_encoding is not None:
                # Compare face encodings
                results = face_recognition.compare_faces([saved_face_encoding], current_encoding, tolerance=0.6)
                if results[0]:
                    cv2.putText(frame, "Match!", (x, y-10), 
                               cv2.FONT_HERSHEY_SIMPLEX, 0.9, (0, 255, 0), 2)
                else:
                    cv2.putText(frame, "No Match", (x, y-10), 
                               cv2.FONT_HERSHEY_SIMPLEX, 0.9, (0, 0, 255), 2)

    # Display instructions
    cv2.putText(frame, "Press 'q' to quit", (10, 30), 
                cv2.FONT_HERSHEY_SIMPLEX, 0.7, (255, 255, 255), 2)

    # Show the frame
    cv2.imshow('Face Detector', frame)

    # Handle key presses
    key = cv2.waitKey(1) & 0xFF
    if key == ord('s') and not face_saved and len(faces) > 0:
        # Save the first detected face
        x, y, w, h = faces[0]
        saved_face_image = save_face_image(frame, (y, x+w, y+h, x))
        saved_face_encoding = get_face_encoding(saved_face_image)
        if saved_face_encoding is not None:
            face_saved = True
            print("Face encoding saved successfully.")
        else:
            print("Error: Could not generate face encoding.")
            os.remove(f"saved_face_{datetime.now().strftime('%Y%m%d_%H%M%S')}.jpg")
    elif key == ord('q'):
        break

# Release resources
cap.release()
cv2.destroyAllWindows()