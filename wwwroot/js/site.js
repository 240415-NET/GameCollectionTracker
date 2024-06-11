document.addEventListener("DOMContentLoaded", () => {
  const newUserContainer = document.querySelector('#NewUserContainer');
  const loginContainer = document.querySelector('#LoginContainer');
  const addGameContainer = document.querySelector('#AddGameContainer');
  const addGamePlayContainer = document.querySelector('#AddGamePlayContainer');
  const reviewPlayHistoryContainer = document.querySelector('#ReviewPlayHistoryContainer');
  const adminFormContainer = document.querySelector('#AdminFormContainer');
  const newUserBtn = document.querySelector("#btnCreateNewUser");
  const resetNewUserFormBtn = document.querySelector("#btnResetForm");
  const foundPlayerContainer = document.querySelector("#FoundPlayerContainer");
  const newUserGamerTag = document.querySelector("#newUserGamerTag");
  const newUserFirstName = document.querySelector("#newUserFirstName");
  const newUserLastName = document.querySelector("#newUserLastName");
  const newUserPassWord = document.querySelector("#newUserPassword");
  const newUserRePassWord = document.querySelector("#newUserRePassword");
  const gamerTagMessage = document.querySelector("#GamerTagMessage");
  const passwordMessage = document.querySelector("#PasswordMessage");
  const rePasswordMessage = document.querySelector("#RePasswordMessage");
  const firstNameMessage = document.querySelector("#FirstNameMessage");
  const lastNameMessage = document.querySelector("#LastNameMessage");

  function resetForm() {
    newUserGamerTag.value = "";
    newUserFirstName.value = "";
    newUserLastName.value = "";
    newUserPassWord.value = "";
    newUserRePassWord.value = "";
    foundPlayerContainer.innerHTML = "";
    gamerTagMessage.textContent = "";
    passwordMessage.textContent = "";
    rePasswordMessage.textContent = "";
    firstNameMessage.textContent = "";
    lastNameMessage.textContent = "";
  }
  function displayPlayers(players) {
    try {
      let allPlayers = "";
      allPlayers += `
    <div>
        <h2>Matching Players</h2>
        <p>Please select which previously added player is you</p>
    </div>`;
      players.forEach((player) => {
        const playerElement = `
        <div class="foundPlayer" data-id="${player.playerID}">
            <p>${player.playerName}</p>
        </div>
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
            <p>None of these are me</p>
        </div>
        `;
        foundPlayerContainer.innerHTML = allPlayers;
        document.querySelectorAll(".foundPlayer").forEach((player) => {
          player.addEventListener("click", function () {
            addNewUserWithPlayer(player.dataset.id);
          });
        });
      }
    } catch (error) {
      console.error("Something went wrong... ", error);
    }
  }

  async function addNewUser() {
    const body = {
      GamerTag: newUserGamerTag.value,
      Password: newUserPassWord.value,
      FirstName: newUserFirstName.value,
      LastName: newUserLastName.value,
    };
    const response = await fetch(`http://localhost:5071/api/User/`, {
      method: "Post",
      body: JSON.stringify(body),
      headers: {
        "content-type": "application/json",
      },
    });
    resetForm();

  }

  async function addNewUserWithPlayer(id) {
    if (id == "NoneOfThem") {
      addNewUser();
    } else {
      const body = {
        GamerTag: newUserGamerTag.value,
        Password: newUserPassWord.value,
        FirstName: newUserFirstName.value,
        LastName: newUserLastName.value,
        playerID: id,
      };
      const response = await fetch(`http://localhost:5071/api/User/`, {
        method: "Post",
        body: JSON.stringify(body),
        headers: {
          "content-type": "application/json",
        },
      });
      resetForm();

    }
  }

  newUserBtn.addEventListener("click", async function () {
    if (newUserGamerTag.value == "") {
      gamerTagMessage.textContent = "Gamer Tag cannot be blank!";
    } else if (newUserPassWord.value == "") {
      passwordMessage.textContent = "Password cannot be blank!";
    } else if (newUserRePassWord.value == "" || newUserRePassWord.value != newUserPassWord.value) {
      rePasswordMessage.textContent =
        "This must match your password and cannot be blank!";
    } else if (newUserFirstName.value == "") {
      firstNameMessage.textContent = "First name cannot be blank!";
    } else if (newUserLastName.value == "") {
      lastNameMessage.textContent = "Last name cannot be blank!";
    } else {
      const response = await fetch(
        `http://localhost:5071/api/User/api/FindPlayers/userInfo?GamerTag=${newUserGamerTag.value}&FirstName=${newUserFirstName.value}&LastName=${newUserLastName.value}`
      );
      const players = await response.json();
      if (players.length == 0) {
        addNewUser();
      } else {
        displayPlayers(players);
      }
    }
  });

  resetNewUserFormBtn.addEventListener("click", function () {
    resetForm();
  });


  //New GameContainer
  //const addGameContainer = document.querySelector('#AddGameContainer');

  // <input type="text" id="addGameName" placeholder="Game Name"><span class="warningText" id="GameNameMessage"></span></br>
  //       <input type="number" id="addGamePurchasePrice" placeholder="Purchase Price"><span class="warningText" id="GamePriceMessage"></span></br>
  //       <input type="date" id="addGamePurchaseDate" placeholder="Purchase Date"><span class="warningText" id="DateMessage"></span></br>
  //       <input type="number" id="addGameMinPlayers" placeholder="Minimum Number of Players"><span class="warningText" id="MinPlayersMessage"></span></br>
  //       <input type="number" id="addGameMaxPlayers" placeholder="Maximum Number of Players"><span class="warningText" id="MaxPlayersMessage"></span></br>
  //       <input type="number" id="addGameExpectedDuration" placeholder="Expected Game Duration"><span class="warningText" id="ExpectedDurationMessage"></span></br>
  const addGameName = document.querySelector();
  const addGamePurchasePrice = document.querySelector();
  const addGamePurchaseDate = document.querySelector();
  const addGameMinPlayers = document.querySelector();
  const addGameMaxPlayers = document.querySelector();
  const addGameExpectedDuration = document.querySelector();

  function resetGameForm() {
    addGameName.value = "";
    addGamePurchasePrice = "";
    addGamePurchaseDate = "";
    addGameMinPlayers = "";
    addGameMaxPlayers = "";
    addGameExpectedDuration = "";
  }

  async function addNewGame() {
    const body = {
      GameName: addGameName.value,
      PurchasePrice: addGamePurchasePrice.value,
      PurchaseDate: addGamePurchaseDate.value,
      MinPlayers: addGameMinPlayers.value,
      MaxPlayers: addGameMaxPlayers.value,
      ExpectedDuration: addGameExpectedDuration.value
    };
    const response = await fetch(`http://localhost:5071/api/Game/`, {
      method: "Post",
      body: JSON.stringify(body),
      headers: {
        "content-type": "application/json",
      },
    });
    resetGameForm();
  }

  newGameBtn.addEventListener("click", async function () {
    if (addGAmeName.value == "") {
      GameNameMessage.textContent = "Game Name cannot be blank!";
    }
    else if (addGamePurchasePrice.value == "") {
      GamePriceMessage.textContent = "Game Price cannot be blank!";
    } else if (addGamePurchaseDate.value == "") {
      lastNameMessage.textContent = "Last name cannot be blank!";
    } else if (addGameMinPlayers == "") {
      MinPlayersMessage.textContent == "Min Players cannot be blank!";
    } else if (addGameMaxPlayers == "") {
      MaxPlayersMessage.textContent == "Min Players cannot be blank!";
    } else if (ExpectedDuration == "") {
      ExpectedDurationMessage.textContent == "Expected Duration cannot be blank!";
    }
    addNewGame();
  });

  resetNewUserFormBtn.addEventListener("click", function () {
    resetForm();
  });
});

