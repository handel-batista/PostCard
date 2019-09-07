app = angular.module('PostCardApp', []);
app.controller('ImageUploadPreviewCtrl', function ($scope, $http) {
   
    $scope.fileList = [];
    $scope.curFile;
    $scope.ImageProperty = {
        file: ''
    };
    $scope.UploadedImages = [];

    //Loads the files in the tbpreview table.
    $scope.setFile = function (element)
    {          
        $scope.fileList = [];

        var files = element.files;
        for (var i = 0; i < files.length; i++) {
            $scope.ImageProperty.file = files[i];

            $scope.fileList.push($scope.ImageProperty);
            $scope.ImageProperty = {};
            $scope.$apply();
        }

        //Checks if the file input is empty. 
        $scope.fileSelectionCheck();
    };

    //deletes the tbpreview table row and clears the selected image
    $scope.deleteRow = function (index)
    {
        if (confirm("Are you sure you want to delete this image?")) {
            document.getElementById('Image').value = '';
            document.getElementById('Iholder').src = '';
            $scope.fileList.splice(index, 1);

            //Disables the submit button.
            $scope.fileSelectionCheck();
        }
    };

    //Deletes an uploaded entry from the database and removes the row from tbSentImages.
    $scope.deleteDatabaseEntry = function (index, btnobject) {
        if (confirm("Are you sure you want to remove this entry from the database?")) {

            $.ajax({
                url: '/Home/DeleteUploadedData?handler=Single',
                method: "PUT",
                dataType: 'json',
                data: { DataId: btnobject.image.DataId },
                contentType: "application/x-www-form-urlencoded; charset=UTF-8"
            }).then(function onSuccess(response) {

                //if the row deletions was successful delete the row from
                //the scope as well.
                if (response !== null) {
                    $scope.$$childHead.UploadedImages.splice(index, 1);
                    $scope.$apply();
                }
                else { console.log('nothing was found'); }

            }).catch(function onError(response) {
                console.log(response);
            });
        }
    };

    //Gets the sent images(if any) when an email address is provided.
    $scope.getUploadedImages = function (email)
    {
        //Checks if the file input is empty.
        $scope.fileSelectionCheck();

        //To display the subject and body warnings.
        $scope.subjectCheck();
        $scope.mailBodyCheck();

        $.ajax({
            url: '/Home/GetUploadedImages?handler=Single',
            method: "PUT",
            dataType: 'json',
            data: { email: email.value },
            contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        }).then(function onSuccess(response) {

            if (response.length > 0) {

                $scope.UploadedImages = [];
                for (var i = 0; i < response.length; i++) {
                    $scope.UploadedImages.push(response[i]);
                    $scope.$apply();
                }
            }
            else
            {
                //Empty the table if no data was found. 
                $scope.UploadedImages = [];
                $scope.$apply();
            }
        }).catch(function onError(response) {
            console.log(response);
        });
    };

    //Displays the empty subject warning.
    $scope.subjectCheck = function ()
    {
        if ($scope.Subject === undefined || $scope.Subject === "") {
            document.getElementById('SubjectDiv').classList.add("text-warning");
            document.getElementById('SubjectSpan').classList.remove("hidden");
        }
        else {
            document.getElementById('SubjectDiv').classList.remove("text-warning");
            document.getElementById('SubjectSpan').classList.add("hidden");
        }
    };

    //Displays the empty email body warning.
    $scope.mailBodyCheck = function ()
    {
        if ($scope.EmailBody === undefined || $scope.EmailBody === "") {
            document.getElementById('EmailBodyDiv').classList.add("text-warning");
            document.getElementById('EmailBodySpan').classList.remove("hidden");
        }
        else {
            document.getElementById('EmailBodyDiv').classList.remove("text-warning");
            document.getElementById('EmailBodySpan').classList.add("hidden");
        }
    };

    //Displays the image error and enables/disables the btnSend button if the form it's not valid.
    $scope.fileSelectionCheck = function () {
        if ($scope.Image === undefined && document.getElementById('Image').value === "") {
            document.getElementById('ImageErrorDiv').classList.remove("hidden");
            $scope.submitDisable = true;
        }
        else
        {
            document.getElementById('ImageErrorDiv').classList.add("hidden");
            if ($scope.Name && $scope.Email && document.getElementById('Image').value !== "") {
                $scope.submitDisable = false;
                EmailForm.$invalid = false;
            }
            else
            {
                $scope.submitDisable = true;
            }
        }
    };
    
});  
