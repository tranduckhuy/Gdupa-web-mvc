

/* global firebase */

const firebaseConfig = {
    apiKey: "AIzaSyDE6yM5qcV7Ew-mSCgS96Xndj9LJVaQAZ0",
    authDomain: "gdupa-2fa82.firebaseapp.com",
    projectId: "gdupa-2fa82",
    storageBucket: "gdupa-2fa82.appspot.com",
    messagingSenderId: "905377519184",
    appId: "1:905377519184:web:b67170ed9db4a2b0fd6355",
    measurementId: "G-PRCMHB6MQL"
};


firebase.initializeApp(firebaseConfig);

// get elements
var uploader1 = document.getElementById('uploader1');
var uploader2 = document.getElementById('uploader2');
var fileButton1 = document.getElementById('productImage1');
var fileButton2 = document.getElementById('productImage2');

// listen for file selection
fileButton1.addEventListener('change', e => addImage(e, "proImage1", "imgDiv1", uploader1));
fileButton2.addEventListener('change', e => addImage(e, "proImage2", "imgDiv2", uploader2));

function addImage(e, imageId, containerId, uploader) {

    // what happened
    console.log('file upload event', e);

    // get file
    var file = e.target.files[0];
    console.log('e.target: ', e.target);
    console.log('e.target.files: ', e.target.files);

    // create a storage ref
    const storageRef = firebase.storage().ref();

    // Handling, image has the same name as the existing image
    const uniqueFilename = generateUniqueFilename(file.name);
    // Upload the file with the unique filename
    var uploadTask = storageRef.child('productImage/' + uniqueFilename).put(file);


    // The part below is largely copy-pasted from the 'Full Example' section from
    // https://firebase.google.com/docs/storage/web/upload-files

    // update progress bar
    uploadTask.on(firebase.storage.TaskEvent.STATE_CHANGED, // or 'state_changed'
        function (snapshot) {
            // Get task progress, including the number of bytes uploaded and the total number of bytes to be uploaded
            var progress = (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
            uploader.value = progress;
            console.log('Upload is ' + progress + '% done');
            switch (snapshot.state) {
                case firebase.storage.TaskState.PAUSED: // or 'paused'
                    console.log('Upload is paused');
                    break;
                case firebase.storage.TaskState.RUNNING: // or 'running'
                    console.log('Upload is running');
                    break;
            }
        }, function (error) {

            // A full list of error codes is available at
            // https://firebase.google.com/docs/storage/web/handle-errors
            switch (error.code) {
                case 'storage/unauthorized':
                    // User doesn't have permission to access the object
                    break;
                case 'storage/canceled':
                    // User canceled the upload
                    break;
                case 'storage/unknown':
                    // Unknown error occurred, inspect error.serverResponse
                    break;
            }
        }, function () {
            // Upload completed successfully, now we can get the download URL
            // save this link somewhere, e.g. put it in an input field
            var downloadURL = uploadTask.snapshot.downloadURL;
            console.log('downloadURL ===>', downloadURL);
            let divLocation = document.getElementById(containerId);

            let oldImg = divLocation.querySelector("img");
            if (oldImg) {
                oldImg.parentNode.removeChild(oldImg);
            }

            let imgElement = document.createElement("img");
            imgElement.style.maxWidth = "200px";
            imgElement.style.maxHeight = "250px";
            imgElement.src = downloadURL;
            divLocation.append(imgElement);
            document.getElementById(imageId).value = downloadURL;
        });
}

function generateUniqueFilename(filename) {
    const timestamp = Date.now();
    const hash = Math.random().toString(36).substring(7);
    return `${hash}_${timestamp}_${filename}`;
}
