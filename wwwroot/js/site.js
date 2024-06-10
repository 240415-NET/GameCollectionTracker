const newUserBtn = document.querySelector("#btnCreateNewUser");
const resetBtn = document.querySelector("#btnResetForm");
const foundPlayerContainer = document.querySelector("#FoundPlayerContainer");
const gamerTag = document.querySelector("#GamerTag");
const firstName = document.querySelector("#FirstName");
const lastName = document.querySelector("#LastName");
const passWord = document.querySelector("#Password");
const rePassWord = document.querySelector("#RePassword");
const gamerTagMessage = document.querySelector("#GamerTagMessage");
const passwordMessage = document.querySelector("#PasswordMessage");
const rePasswordMessage = document.querySelector("#RePasswordMessage");
const firstNameMessage = document.querySelector("#FirstNameMessage");
const lastNameMessage = document.querySelector("#LastNameMessage");

function resetForm() {
  gamerTag.value = "";
  firstName.value = "";
  lastName.value = "";
  passWord.value = "";
  rePassWord.value = "";
  foundPlayerContainer.innerHTML = "";
  gamerTagMessage.innerHTML = "";
  //gamerTagMessage.classList.add('hidden');
  passwordMessage.innerHTML = "";
  //passwordMessage.classList.add('hidden');
  rePasswordMessage.innerHTML = "";
  //rePasswordMessage.classList.add('hidden');
  firstNameMessage.innerHTML = "";
  //firstNameMessage.classList.add('hidden');
  lastNameMessage.innerHTML = "";
  //lastNameMessage.classList.add('hidden');
}
function displayPlayers(players) {
  try {
    if (players == null || players == undefined) {
      addNewUser();
    } else {
      let allPlayers = "";
      allPlayers += `
    <div>
        <h2>Matching Players</h2>
        <p>Please select which previously added player is you</p>
    </div>`;
      players.forEach((player) => {
        const playerElement = `
        <div class="foundPlayer" data-id="${player.playerID}">
            <p>${player.playerName}<p>
        <div>
        `;
        allPlayers += playerElement;
      });
      if (
        allPlayers !=
        `
    <div>
        <h2>Matching Players<h2>
        <p>Please select which previously added player is you</p>
    </div>`
      ) {
        allPlayers += `
          <div class="foundPlayer" data-id="NoneOfThem">
            <p>None of these are me<p>
        <div>
        `;
        foundPlayerContainer.innerHTML = allPlayers;
        document.querySelectorAll(".foundPlayer").forEach((player) => {
          player.addEventListener("click", function () {
            addNewUserWithPlayer(player.dataset.id);
          });
        });
      }
    }
  } catch {addNewUser();}
}

function addNewUser() {
  const body = {
    GamerTag: gamerTag.value,
    Password: passWord.value,
    FirstName: firstName.value,
    LastName: lastName.value,
  };
  fetch(`http://localhost:5071/api/User/`, {
    method: "Post",
    body: JSON.stringify(body),
    headers: {
      "content-type": "application/json",
    },
  }).then((response) => {
    resetForm();
  });
}

function addNewUserWithPlayer(id) {
  if (id == "NoneOfThem") {
    addNewUser();
  } else {
    const body = {
      GamerTag: gamerTag.value,
      Password: passWord.value,
      FirstName: firstName.value,
      LastName: lastName.value,
      playerID: id,
    };
    fetch(`http://localhost:5071/api/User/`, {
      method: "Post",
      body: JSON.stringify(body),
      headers: {
        "content-type": "application/json",
      },
    }).then((response) => {
      resetForm();
    });
  }
}

newUserBtn.addEventListener("click", function () {
  if (gamerTag.value == "") {
    gamerTagMessage.innerHTML = "Gamer Tag cannot be blank!";
  }
  else if (passWord.value == "") {
    passwordMessage.innerHTML = "Password cannot be blank!";
  }
  else if (rePassWord.value == "" || rePassWord.value != passWord.value) {
    rePasswordMessage.innerHTML = "This must match your password and cannot be blank!";
  }
  else if (firstName.value == "") {
    firstNameMessage.innerHTML = "First name cannot be blank!";
  }
  else if (lastName.value == "") {
    lastNameMessage.innerHTML = "Last name cannot be blank!";
  }
else{
  fetch(
    `http://localhost:5071/api/User/api/FindPlayers/userInfo?GamerTag=${gamerTag.value}&FirstName=${firstName.value}&LastName=${lastName.value}`
  )
    .then((data) => data.json())
    .then((response) => displayPlayers(response)); //{
  //if (response.status == 200) {

  //}
  //});
}});

resetBtn.addEventListener("click", function () {
  resetForm();
});
