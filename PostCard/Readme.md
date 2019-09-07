Post Card 
This application takes an image input from the user (file upload, or camera), 
modifies the image with text, and sends it as an email to a specified recipient. 


Instructions/Help
This app comes with a full on-screen help/instructions guide that shows how to use the application.
Simply click the help link on the top menu bar to start the on-screen tour.

Instructions summary
1. Select or capture the image that you want to edit. 
   You can use the choose file button get to select an image from your computer
   Or click the camera icon to capture an image with your webcam. 
   A camera must be installed on your computer for this feature to work, as well, 
   you must grand the browser access to your camera.

2. Once the image is selected or capture, you can use the Image test input to add text to your image;
   the text is limited to the image with. 

   The Image Queue area table gets populated only when an image is selected using the choose file button.
   

3. Next, you need to complete the user's information form to send the edited image as an attachment
   to the provided email address. The Name, Email, and image are required to submit the form;
   the Subject and the Body are optional.

   The Image(s) Sent area table gets populated when a previously used email is provided.

4. Once all the required fields are provided, as indicated above, the send button will be
   automatically enabled allowing you to submit the form.

5. Click the send button to send your edit image.
    

Running the tests
The application is being tested with xUnit. To test it simply run the PostCard.xUnitTest 
project included. The project test the PostCard class(UploadedDataViewModel) that validates
the controls on the submission form.

For example, the PostCard.xUnitTest includes a named Test_UploadedDataViewModel_FormValidation_required_fields_Should_Pass.
This test is testing that all the required fields are being validated when the form is submitted.



Built With
ASP.NET Core 2.1, AngularJS frameworks, SQL server.

Note:
Use the Add-Migration and update-database command on the Package Manager 
console to create the database if it's not created automatically.


Author
Handel Batista



Acknowledgments
Had tip for the creators of Intro.js a free and open-source library. 
I've used this library to create the help instructions of the site. 

See the Github repository below
https://github.com/usablica/intro.js