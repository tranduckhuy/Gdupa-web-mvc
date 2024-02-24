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
var fileButtonAvatar = document.getElementById('avatarFile');
var fileButtonBackground = document.getElementById('backgroundFile');

// listen for file selection
fileButtonAvatar.addEventListener('change', e => avatarImage(e, "avatarImage"));
fileButtonBackground.addEventListener('change', e => backgroundImage(e, "backgroundImage"));

function avatarImage(e, imageId) {
    // get file
    var file = e.target.files[0];

    // create a storage ref
    const storageRef = firebase.storage().ref();

    // Handling, image has the same name as the existing image
    const uniqueFilename = generateUniqueFilename(file.name);
    // Upload the file with the unique filename
    var uploadTask = storageRef.child('avatar/' + uniqueFilename).put(file);

    // The part below is largely copy-pasted from the 'Full Example' section from
    // https://firebase.google.com/docs/storage/web/upload-files

    // update progress bar
    uploadTask.on(firebase.storage.TaskEvent.STATE_CHANGED, // or 'state_changed'
        function () { },
        function (error) {
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

            var avatarImage = document.getElementById(imageId);

            avatarImage.src = downloadURL;
            document.getElementById('avatarHidden').value = downloadURL;
        });
}

function backgroundImage(e, imageId) {
    // get file
    var file = e.target.files[0];

    // create a storage ref
    const storageRef = firebase.storage().ref();

    // Handling, image has the same name as the existing image
    const uniqueFilename = generateUniqueFilename(file.name);
    // Upload the file with the unique filename
    var uploadTask = storageRef.child('supplier-background/' + uniqueFilename).put(file);

    // The part below is largely copy-pasted from the 'Full Example' section from
    // https://firebase.google.com/docs/storage/web/upload-files

    // update progress bar
    uploadTask.on(firebase.storage.TaskEvent.STATE_CHANGED, // or 'state_changed'
        function () { },
        function (error) {
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

            var backgroundImage = document.getElementById(imageId);

            backgroundImage.src = downloadURL;
            document.getElementById('backgroundHidden').value = downloadURL;
        });
}
function generateUniqueFilename(filename) {
    const timestamp = Date.now();
    const hash = Math.random().toString(36).substring(7);
    return `${hash}_${timestamp}_${filename}`;
}
