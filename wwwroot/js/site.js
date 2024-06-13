document.addEventListener("DOMContentLoaded", () => {
  //login page stuffs
  const loginContainer = document.querySelector("#LoginContainer");
  const loginButton = document.querySelector("#btnLogin");
  const createUserButton = document.querySelector("#btnCreateUser");
  const loginGamerTag = document.querySelector("#LoginGamerTag");
  const loginPassWord = document.querySelector("#LoginPassword");
  const loginGamerTagError = document.querySelector("#LoginGamerTagError");
  const loginPasswordError = document.querySelector("#LoginPasswordError");

  //MainMenu stuffs
  const logoutButton = document.querySelector("#btnLogout");

  //future implementation stuffs
  const gamesContainer = document.querySelector("#games_container");
  const gamePlayContainer = document.querySelector("#GamePlayContainer");
  const playHistoryContainer = document.querySelector("#PlayHistoryContainer");
  const adminMenu = document.querySelector("#AdminMenu");
  const loggedInMenu = document.querySelector("#LoggedInMenu");

  //create new user stuffs
  const newUserContainer = document.querySelector("#NewUserContainer");
  const newUserBtn = document.querySelector("#btnCreateNewUser");
  const resetNewUserFormBtn = document.querySelector("#btnResetForm");
  const newUserBackToLoginBtn = document.querySelector("#btnBackToLogin");
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

  //For a reload with a user logged in
  const storedUser = JSON.parse(localStorage.getItem("user"));
  if (storedUser) {
    UserIsLoggedIn();
  }

  function resetNewUserForm() {
    newUserGamerTag.value = "";
    newUserFirstName.value = "";
    newUserLastName.value = "";
    newUserPassWord.value = "";
    newUserRePassWord.value = "";
    foundPlayerContainer.innerHTML = "";
    resetNewUserWarnings();
  }

  function resetNewUserWarnings() {
    gamerTagMessage.textContent = "";
    passwordMessage.textContent = "";
    rePasswordMessage.textContent = "";
    firstNameMessage.textContent = "";
    lastNameMessage.textContent = "";
  }

  function resetLoginForm(){
    loginGamerTag.value = "";
    loginPassWord.value = "";
    resetLoginWarnings();
  }

  function resetLoginWarnings() {
    loginGamerTagError.textContent = "";
    loginPasswordError.textContent = "";
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
      method: "POST",
      body: JSON.stringify(body),
      headers: {
        "content-type": "application/json",
      },
    });
    if (response.status != 200) {
      gamerTagMessage.textContent = await response.text();
    } else {
      LogInUser(newUserGamerTag.value, newUserPassWord.value);
      //resetNewUserForm();
    }
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
        method: "POST",
        body: JSON.stringify(body),
        headers: {
          "content-type": "application/json",
        },
      });
      if (response.status != 200) {
        gamerTagMessage.textContent = await response.text();
      } else {
        LogInUser(newUserGamerTag.value, newUserPassWord.value);
        //resetNewUserForm();
      }
    }
  }

  async function LogInUser(UserName, UserPass) {
    const body = {
      userName: UserName,
      userPass: UserPass,
    };
    const response = await fetch(`http://localhost:5071/api/User/Login`, {
      method: "POST",
      body: JSON.stringify(body),
      headers: {
        "content-type": "application/json",
      },
    });
    if (response.status == 200) {
      data = await response.json();
      const userStorage = {
        UserID: data.userID,
        GamerTag: data.gamerTag,
        PlayerID: data.playerRecord.playerID,
        IsAdmin: data.isAdmin
      };
      localStorage.setItem("user", JSON.stringify(userStorage));
    } else if (response.status == 404) {
      loginGamerTagError.textContent = "Account does not exist";
    } else {
      loginPasswordError.textContent = "Password is not correct.";
    }
    resetLoginForm();
    UserIsLoggedIn();
  }

  function UserIsLoggedIn() {
    loginContainer.classList.add("hidden");
    newUserContainer.classList.add("hidden");
    const Greeting = document.getElementById("UserGreeting");
    Greeting.textContent =
      JSON.parse(localStorage.getItem("user")).GamerTag + "'s Games";
    loggedInMenu.classList.remove("hidden");
    if(JSON.parse(localStorage.getItem("user")).IsAdmin)
      {
        adminMenu.classList.remove("hidden");
      }else{
        adminMenu.classList.add("hidden");
      }
  }

  function DisplayUsersGames(games)
  {
    let allGames = '';
    games.forEach(game => {
      const gameElement = `
      <div class="game" data-id="${game.GameID}">
      <h3>${game.GameName}</h3>
      <p>Min Players: ${game.MinPlayers}</p>
      <p>Max Players: ${game.MaxPlayers}</p>
      <p>Play Time: ${game.ExpectedGameDuration} minutes</p>
      <p>Purchase Date: ${game.PurchaseDate}</p>
      <p>Purchase Price: $${game.PurchasePrice}</p>
      </div>`;
      allGames += gameElement;
    });
    gamesContainer.innerHTML = allGames;

    document.querySelectorAll('.game').forEach(game => {
      game.addEventListener('click', function () {
        UsersGameClick(game.dataset.id);
      });
    })
  }
  function UsersGameClick(id) {

  }

  newUserBtn.addEventListener("click", async function () {
    resetNewUserWarnings();
    if (!newUserGamerTag.value) {
      gamerTagMessage.textContent = "Gamer Tag cannot be blank!";
    } else if (!newUserPassWord.value) {
      passwordMessage.textContent = "Password cannot be blank!";
    } else if (
      !newUserRePassWord.value ||
      newUserRePassWord.value != newUserPassWord.value
    ) {
      rePasswordMessage.textContent =
        "This must match your password and cannot be blank!";
    } else if (!newUserFirstName.value) {
      firstNameMessage.textContent = "First name cannot be blank!";
    } else if (!newUserLastName.value) {
      lastNameMessage.textContent = "Last name cannot be blank!";
    } else {
      const response = await fetch(
        `http://localhost:5071/api/User/FindPlayers/userInfo?GamerTag=${newUserGamerTag.value}&FirstName=${newUserFirstName.value}&LastName=${newUserLastName.value}`
      );
      if (response.status == 404) {
        addNewUser();
      } else {
        const players = await response.json();
        displayPlayers(players);
      }
    }
  });

  resetNewUserFormBtn.addEventListener("click", function () {
    resetNewUserForm();
  });

  loginButton.addEventListener("click", async () => {
    resetLoginWarnings();
    if (!loginGamerTag.value) {
      loginGamerTagError.textContent = "Gamer Tag cannot be blank!";
    } else if (!loginPassWord.value) {
      loginPasswordError.textContent = "Password cannot be blank!";
    }
    LogInUser(loginGamerTag.value, loginPassWord.value);
  });

  createUserButton.addEventListener("click", () => {
    loginContainer.classList.add("hidden");
    newUserContainer.classList.remove("hidden");
  });

  newUserBackToLoginBtn.addEventListener("click", () => {
    resetNewUserForm();
    loginContainer.classList.remove("hidden");
    newUserContainer.classList.add("hidden");
  });

  logoutButton.addEventListener("click", () => {
    localStorage.removeItem("user");
    loginContainer.classList.remove("hidden");
    loggedInMenu.classList.add("hidden");
    adminMenu.classList.add("hidden");
  });

  //New GameContainer
  const addGameName = document.querySelector('#addGameName');
  const addGamePurchasePrice = document.querySelector('#addGamePurchasePrice');
  const addGamePurchaseDate = document.querySelector("#addGamePurchaseDate");
  const addGameMinPlayers = document.querySelector("#addGameMinPlayers");
  const addGameMaxPlayers = document.querySelector("#addGameMaxPlayers");
  const addGameExpectedDuration = document.querySelector("#addGameExpectedDuration");
  const newGameBtn = document.querySelector("#btnCreateNewGame");
  const btnResetGameForm = document.querySelector("#btnResetGameForm");
  const GameNameMessage = document.querySelector("#GameNameMessage");
  const GamePriceMessage = document.querySelector("#GamePriceMessage");
  const GamePurchaseDateMessage = document.querySelector("#GamePurchaseDateMessage");
  const MinPlayersMessage = document.querySelector("#MinPlayersMessage");
  const MaxPlayersMessage = document.querySelector("#MaxPlayersMessage");
  const ExpectedDurationMessage = document.querySelector("#ExpectedDurationMessage")


  function resetGameForm() {
    addGameName.value = "";
    addGamePurchasePrice.value = "";
    addGamePurchaseDate.value = "";
    addGameMinPlayers.value = "";
    addGameMaxPlayers.value = "";
    addGameExpectedDuration.value = "";
  }

  async function addNewGame() {
    const body = {
      GameName: addGameName.value,
      PurchasePrice: parseFloat(addGamePurchasePrice.value),
      PurchaseDate: addGamePurchaseDate.value,
      MinPlayers: parseFloat(addGameMinPlayers.value),
      MaxPlayers: parseFloat(addGameMaxPlayers.value),
      ExpectedDuration: parseFloat(addGameExpectedDuration.value)
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
    if (addGameName.value == "") {
      GameNameMessage.textContent = "Game Name cannot be blank!";
    }
    else if (addGamePurchasePrice.value == "") {
      GamePriceMessage.textContent = "Game Price cannot be blank!";
    } else if (addGamePurchaseDate.value == "") {
      GamePurchaseDateMessage.textContent = "Purchase Date cannot be blank!";
    } else if (addGameMinPlayers == "") {
      MinPlayersMessage.textContent == "Min Players cannot be blank!";
    } else if (addGameMaxPlayers == "") {
      MaxPlayersMessage.textContent == "Min Players cannot be blank!";
    } else if (addGameExpectedDuration == "") {
      ExpectedDurationMessage.textContent == "Expected Duration cannot be blank!";
    }
    addNewGame();
  });

  btnResetGameForm.addEventListener("click", function () {
    resetGameForm();
  });
});

