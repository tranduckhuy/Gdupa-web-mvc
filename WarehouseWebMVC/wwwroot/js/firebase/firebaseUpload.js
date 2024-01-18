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
var fileButton1 = document.getElementById('productImage1');
var fileButton2 = document.getElementById('productImage2');

// Function to create ProgressBar
function createProgressBar(containerId) {
    return new ProgressBar.Line(containerId, {
        strokeWidth: 4,
        easing: 'easeInOut',
        duration: 1400,
        color: '#FFEA82',
        trailColor: '#eee',
        trailWidth: 1,
        svgStyle: { width: '100%', height: '100%' },
        text: {
            style: {
                color: '#999',
                position: 'absolute',
                left: '40%',
                top: '8px',
                padding: 0,
                margin: 0,
                transform: 'translateX(0, -50%)'
            },
            autoStyleContainer: false
        },
        from: { color: '#FFEA82' },
        to: { color: '#ED6A5A' },
        step: (state, bar) => {
            bar.setText(Math.round(bar.value() * 100) + ' %');
        }
    });
}

// listen for file selection
fileButton1.addEventListener('change', e => addImage(e, "proImage1", "imgDiv1", "progress-container1"));
fileButton2.addEventListener('change', e => addImage(e, "proImage2", "imgDiv2", "progress-container2"));

function addImage(e, imageId, containerId, progressBarContainer) {
    // get file
    var file = e.target.files[0];

    // create a storage ref
    const storageRef = firebase.storage().ref();

    // Handling, image has the same name as the existing image
    const uniqueFilename = generateUniqueFilename(file.name);
    // Upload the file with the unique filename
    var uploadTask = storageRef.child('productImage/' + uniqueFilename).put(file);

    // Display the progress bar
    progressBarContainer.innerHTML = '';
    var progressBar = createProgressBar('#' + progressBarContainer);

    // The part below is largely copy-pasted from the 'Full Example' section from
    // https://firebase.google.com/docs/storage/web/upload-files

    // update progress bar
    uploadTask.on(firebase.storage.TaskEvent.STATE_CHANGED, // or 'state_changed'
        function () {
            progressBar.animate(1.0); // Number from 0.0 to 1.0
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

            var divProgressBar = document.getElementById(progressBarContainer);
            setTimeout(function () {
                divProgressBar.innerHTML = '';
            }, 1500)
        });
}

function generateUniqueFilename(filename) {
    const timestamp = Date.now();
    const hash = Math.random().toString(36).substring(7);
    return `${hash}_${timestamp}_${filename}`;
}
