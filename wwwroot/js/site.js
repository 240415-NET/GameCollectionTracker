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
  const addGameButton = document.querySelector("#btnAddNewGame");
  const addGameButtonBox = document.querySelector("#AddGameButtonBox");
  const updateGameButton = document.querySelector("#btnUpdateGame");
  const updateGameButtonBox = document.querySelector("#UpdateGameButtonBox");
  const removeGameButton = document.querySelector("#btnRemoveGame");
  const removeGameButtonBox = document.querySelector("#RemoveGameButtonBox");
  const clearSelectionButton = document.querySelector("#btnClearSelection");
  const clearSelectionButtonBox = document.querySelector(
    "#ClearSelectionButtonBox"
  );
  const adminMenu = document.querySelector("#AdminMenu");
  const loggedInMenu = document.querySelector("#LoggedInMenu");
  const gamesContainer = document.querySelector("#myGamesContainer");
  const recordGamePlayButton = document.querySelector("#btnRecordNewGamePlay");
  const recordGamePlayButtonBox = document.querySelector(
    "#RecordGamePlayButtonBox"
  );
  const mergePlayerButton = document.querySelector("#MergePlayerBtn");
  const manageAdminButton = document.querySelector("#AdminStatusBtn");

  //future implementation stuffs
  const gamePlayContainer = document.querySelector("#GamePlayContainer");

  //this is play history stuff
  const playHistoryContainer = document.querySelector("#PlayHistoryContainer");
  const viewPlayHistoryButton = document.querySelector("#btnViewPlayHistory");
  //added the button to return to main and the boxes to control visibility (boxes are so we can hide the breaks as well)
  const returnToMainButtonBox = document.querySelector(
    "#ReturnFromPlayHistoryButtonBox"
  );
  const returnToMainButton = document.querySelector(
    "#btnReturnFromPlayHistory"
  );
  const viewPlayHistoryButtonBox = document.querySelector(
    "#ViewPlayHistoryButtonBox"
  );

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
  function hideSideBarButtons() {
    addGameButtonBox.classList.add("hidden");
    updateGameButtonBox.classList.add("hidden");
    removeGameButtonBox.classList.add("hidden");
    clearSelectionButtonBox.classList.add("hidden");
    viewPlayHistoryButtonBox.classList.add("hidden");
    adminMenu.classList.add("hidden");
    recordGamePlayButtonBox.classList.add("hidden");
  }

  function showSideBarButtons() {
    resetGameSelection();
    addGameButtonBox.classList.remove("hidden");
    viewPlayHistoryButtonBox.classList.remove("hidden");
    if (JSON.parse(localStorage.getItem("user")).IsAdmin) {
      adminMenu.classList.remove("hidden");
    }
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

  function resetLoginForm() {
    loginGamerTag.value = "";
    loginPassWord.value = "";
    resetLoginWarnings();
  }

  function resetLoginWarnings() {
    loginGamerTagError.textContent = "";
    loginPasswordError.textContent = "";
  }

  async function getGamesOwnedByUser() {
    const response = await fetch(
      `http://localhost:5071/Games/${
        JSON.parse(localStorage.getItem("user")).UserID
      }`
    );
    if (response.status == 200) {
      const responsedata = await response.json();
      DisplayUsersGames(responsedata.selectedGames);
    }
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
      }
    }
  }

  async function LogInUser(UserName, UserPass) {
    resetLoginWarnings();
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
        IsAdmin: data.isAdmin,
      };
      localStorage.setItem("user", JSON.stringify(userStorage));
      UserIsLoggedIn();
      resetLoginForm();
    } else if (response.status == 404) {
      loginGamerTagError.textContent = "Account does not exist";
    } else {
      loginPasswordError.textContent = "Password is not correct.";
    }
  }

  function UserIsLoggedIn() {
    loginContainer.classList.add("hidden");
    newUserContainer.classList.add("hidden");
    const Greeting = document.getElementById("UserGreeting");
    Greeting.textContent =
      JSON.parse(localStorage.getItem("user")).GamerTag + "'s Games";
    loggedInMenu.classList.remove("hidden");
    if (JSON.parse(localStorage.getItem("user")).IsAdmin) {
      adminMenu.classList.remove("hidden");
    } else {
      adminMenu.classList.add("hidden");
    }
    getGamesOwnedByUser();
    resetGameSelection();
  }

  function DisplayUsersGames(games) {
    let allGames = "";
    games.forEach((game) => {
      const gameElement = `
      <div class="game" data-id="${game.gameID}">
      <h2>${game.gameName}</h2>
      <p>Min Players: ${game.minPlayers}</p>
      <p>Max Players: ${game.maxPlayers}</p>
      <p>Play Time: ${game.expectedGameDuration} minutes</p>
      <p>Purchase Date: ${game.purchaseDate}</p>
      <p>Purchase Price: $${game.purchasePrice}</p>
      </div>`;
      allGames += gameElement;
    });
    gamesContainer.innerHTML = allGames;

    document.querySelectorAll(".game").forEach((game) => {
      game.addEventListener("click", function () {
        UsersGameClick(game.dataset.id);
      });
    });
  }

  async function UsersGameClick(id) {
    resetGameSelection();
    let selectedGameID = "";
    const unselectedGameItems = document
      .querySelectorAll(".game")
      .forEach((game) => {
        if (game.dataset.id == id) {
          selectedGameID = game.dataset.id;
          game.classList.remove("game");
          game.classList.add("Selectedgame");
        }
      });
    const response = await fetch(
      `http://localhost:5071/Games?gameIdToFindFromFrontEnd=${selectedGameID}`
    );
    const gameData = await response.json();
    const gameToAdd = {
      gameID: selectedGameID,
      gameName: gameData.gameName,
      minPlayers: gameData.minPlayers,
      maxPlayers: gameData.maxPlayers,
      expectedPlayTime: gameData.expectedGameDuration,
      purchaseDate: gameData.purchaseDate,
      purchasePrice: gameData.purchasePrice,
    };

    localStorage.setItem("selectedGame", JSON.stringify(gameToAdd));
    updateGameButtonBox.classList.remove("hidden");
    removeGameButtonBox.classList.remove("hidden");
    clearSelectionButtonBox.classList.remove("hidden");
    recordGamePlayButtonBox.classList.remove("hidden");
    addGameButtonBox.classList.add("hidden");
  }

  removeGameButton.addEventListener("click", function () {
    if (
      confirm(
        "All recorded game plays for this game will also be deleted. Are you sure you want to remove it?"
      )
    ) {
      const response = fetch(
        `http://localhost:5071/api/Game/Remove?GameId=${
          JSON.parse(localStorage.getItem("selectedGame")).gameID
        }`,
        { method: "DELETE" }
      );
      localStorage.removeItem("selectedGame");
    }
    location.reload();
  });

  updateGameButton.addEventListener("click", function () {
    
    gamesContainer.style.display = "none";
    addGameContainer.classList.remove("hidden");
    addUpdateGameHeader.textContent = "Update Game";
    newGameBtn.classList.add("hidden");
    btnResetGameForm.classList.add("hidden");
    btnResetUpdateForm.classList.remove("hidden");
    btnSubmitUpdateGame.classList.remove("hidden");
    btnCancelNewGame.classList.add("hidden");
    btnCancelUpdateGame.classList.remove("hidden");
    btnResetUpdateForm.addEventListener("click", function () {
      PopulateUpdateGameForm();
    });

    btnCancelUpdateGame.addEventListener("click", function () {
      resetGameForm();
      resetGameUpdate();
      showSideBarButtons();
    });
    PopulateUpdateGameForm();
    btnSubmitUpdateGame.addEventListener("click", function () {
      if (addGameName.value == "") {
        GameNameMessage.textContent = "Game Name cannot be blank!";
      } else if (addGamePurchasePrice.value == "") {
        GamePriceMessage.textContent = "Game Price cannot be blank!";
      } else if (addGamePurchaseDate.value == "") {
        GamePurchaseDateMessage.textContent = "Purchase Date cannot be blank!";
      } else if (addGameMinPlayers == "") {
        MinPlayersMessage.textContent == "Min Players cannot be blank!";
      } else if (addGameMaxPlayers == "") {
        MaxPlayersMessage.textContent == "Min Players cannot be blank!";
      } else if (addGameExpectedDuration == "") {
        ExpectedDurationMessage.textContent ==
          "Expected Duration cannot be blank!";
      } else {
        UpdateSelectedGame();
        resetGameSelection();
        location.reload();
      }
    });
    hideSideBarButtons();
  });

  async function UpdateSelectedGame() {
    const body = {
      gameID: JSON.parse(localStorage.getItem("selectedGame")).gameID,
      gameName: addGameName.value,
      purchasePrice: parseFloat(addGamePurchasePrice.value),
      purchaseDate: addGamePurchaseDate.value,
      minPlayers: parseInt(addGameMinPlayers.value),
      maxPlayers: parseInt(addGameMaxPlayers.value),
      expectedGameDuration: parseInt(addGameExpectedDuration.value),
    };
    const response = await fetch(`http://localhost:5071/api/Game`, {
      method: "PATCH",
      body: JSON.stringify(body),
      headers: {
        "content-type": "application/json",
      },
    });
    if ((response.status = 200)) {
      resetGameForm();
      resetGameUpdate();
    }
  }

  function PopulateUpdateGameForm() {
    addGameName.value = JSON.parse(
      localStorage.getItem("selectedGame")
    ).gameName;
    addGamePurchasePrice.value = JSON.parse(
      localStorage.getItem("selectedGame")
    ).purchasePrice;
    addGamePurchaseDate.value = JSON.parse(
      localStorage.getItem("selectedGame")
    ).purchaseDate;
    addGameMinPlayers.value = JSON.parse(
      localStorage.getItem("selectedGame")
    ).minPlayers;
    addGameMaxPlayers.value = JSON.parse(
      localStorage.getItem("selectedGame")
    ).maxPlayers;
    addGameExpectedDuration.value = JSON.parse(
      localStorage.getItem("selectedGame")
    ).expectedPlayTime;
    resetGameFormWarnings();
  }

  function resetGameUpdate() {
    gamesContainer.style.display = "flex";
    addGameContainer.classList.add("hidden");
    addUpdateGameHeader.textContent = "Add a New Game";
    newGameBtn.classList.remove("hidden");
    btnResetGameForm.classList.remove("hidden");
    btnResetUpdateForm.classList.add("hidden");
    btnSubmitUpdateGame.classList.add("hidden");
    btnCancelNewGame.classList.remove("hidden");
    btnCancelUpdateGame.classList.add("hidden");
  }

  function resetGameSelection() {
    const selectedGameItems = document.getElementsByClassName("Selectedgame");
    if (selectedGameItems.length > 0) {
      selectedGameItems[0].classList.replace("Selectedgame", "game");
    }
    localStorage.removeItem("selectedGame");
  }

  clearSelectionButton.addEventListener("click", function () {
    resetGameSelection();
    updateGameButtonBox.classList.add("hidden");
    removeGameButtonBox.classList.add("hidden");
    clearSelectionButtonBox.classList.add("hidden");
    recordGamePlayButtonBox.classList.add("hidden");
    addGameButtonBox.classList.remove("hidden");
  });

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
    gamesContainer.innerHTML = "";
    gamesContainer.style.display = "flex";
    addGameContainer.classList.add("hidden");
  });

  addGameButton.addEventListener("click", () => {
    hideSideBarButtons();
    gamesContainer.style.display = "none";
    addUpdateGameHeader.textContent = "Add a New Game";
    addGameContainer.classList.remove("hidden");
  });

  //New GameContainer
  const addGameContainer = document.querySelector("#AddGameContainer");
  const addUpdateGameHeader = document.querySelector("#AddUpdateHeader");
  const addGameName = document.querySelector("#addGameName");
  const addGamePurchasePrice = document.querySelector("#addGamePurchasePrice");
  const addGamePurchaseDate = document.querySelector("#addGamePurchaseDate");
  const addGameMinPlayers = document.querySelector("#addGameMinPlayers");
  const addGameMaxPlayers = document.querySelector("#addGameMaxPlayers");
  const addGameExpectedDuration = document.querySelector(
    "#addGameExpectedDuration"
  );
  const newGameBtn = document.querySelector("#btnCreateNewGame");
  const btnResetGameForm = document.querySelector("#btnResetGameForm");
  const btnResetUpdateForm = document.querySelector("#btnResetUpdateGame");
  const btnSubmitUpdateGame = document.querySelector("#btnUpdateGameForm");
  const btnCancelUpdateGame = document.querySelector("#btnCancelUpdateGame");
  const GameNameMessage = document.querySelector("#GameNameMessage");
  const GamePriceMessage = document.querySelector("#GamePriceMessage");
  const GamePurchaseDateMessage = document.querySelector(
    "#GamePurchaseDateMessage"
  );
  const MinPlayersMessage = document.querySelector("#MinPlayersMessage");
  const MaxPlayersMessage = document.querySelector("#MaxPlayersMessage");
  const ExpectedDurationMessage = document.querySelector(
    "#ExpectedDurationMessage"
  );
  const btnCancelNewGame = document.querySelector("#btnCancelNewGame");

  function resetGameForm() {
    addGameName.value = "";
    addGamePurchasePrice.value = "";
    addGamePurchaseDate.value = "";
    addGameMinPlayers.value = "";
    addGameMaxPlayers.value = "";
    addGameExpectedDuration.value = "";
    resetGameFormWarnings();
  }

  function resetGameFormWarnings() {
    GameNameMessage.textContent = "";
    GamePriceMessage.textContent = "";
    GamePurchaseDateMessage.textContent = "";
    MinPlayersMessage.textContent = "";
    MaxPlayersMessage.textContent = "";
    ExpectedDurationMessage.textContent = "";
  }

  async function addNewGame() {
    const body = {
      UserID: JSON.parse(localStorage.getItem("user")).UserID,
      GameName: addGameName.value,
      PurchasePrice: parseFloat(addGamePurchasePrice.value),
      PurchaseDate: addGamePurchaseDate.value,
      MinPlayers: parseInt(addGameMinPlayers.value),
      MaxPlayers: parseInt(addGameMaxPlayers.value),
      expectedGameDuration: parseInt(addGameExpectedDuration.value),
    };
    const response = await fetch(`http://localhost:5071/api/Game/`, {
      method: "Post",
      body: JSON.stringify(body),
      headers: {
        "content-type": "application/json",
      },
    });
    resetGameForm();
    gamesContainer.style.display = "flex";
    addGameContainer.classList.add("hidden");
    location.reload();
  }

  newGameBtn.addEventListener("click", async function () {
    resetGameFormWarnings();
    if (!addGameName.value) {
      GameNameMessage.textContent = "Game Name cannot be blank!";
    } else if (!addGamePurchasePrice.value) {
      GamePriceMessage.textContent = "Game Price cannot be blank!";
    } else if (!addGamePurchaseDate.value) {
      GamePurchaseDateMessage.textContent = "Purchase Date cannot be blank!";
    } else if (!addGameMinPlayers.value) {
      MinPlayersMessage.textContent = "Min Players cannot be blank!";
    } else if (!addGameMaxPlayers.value) {
      MaxPlayersMessage.textContent = "Min Players cannot be blank!";
    } else if (!addGameExpectedDuration.value) {
      ExpectedDurationMessage.textContent =
        "Expected Duration cannot be blank!";
    } else {
      addNewGame();
    }
  });

  btnResetGameForm.addEventListener("click", function () {
    resetGameForm();
  });

  btnCancelNewGame.addEventListener("click", function () {
    showSideBarButtons();
    gamesContainer.style.display = "flex";
    addGameContainer.classList.add("hidden");
    getGamesOwnedByUser();
  });

  //Admin Functions
  const adminUserContainer = document.querySelector(
    "#AdminFunctionUserContainer"
  );
  const adminPlayerContainer = document.querySelector(
    "#AdminFunctionPlayerContainer"
  );
  const mergePlayerButtonBox = document.querySelector("#MergePlayerButtonBox");
  const mergeSelectedPlayerButton = document.querySelector("#mergePlayerBtn");

  async function getUsersForAdmin(forAdmin) {
    const response = await fetch(
      `http://localhost:5071/api/User/GetUsersForAdminStatus`
    );
    try {
      const responseData = await response.json();
      if (forAdmin) {
        displayUsersForAdmin(responseData);
      } else {
        displayUsersForMerge(responseData);
      }
    } catch (error) {
      let userHTML = `<div class = "userAdminHeader">
    <h1>Current Users</h1>
    </div>
    <div class = "userAdmin">
      <h2>No users?</h2>
      <p>You have no friends</p>
    `;
    adminUserContainer.innerHTML = userHTML;
    }
  }

  async function getPlayersForAdmin() {
    const response = await fetch(
      `http://localhost:5071/api/User/GetMergePlayers`
    );
    try {
      const responseData = await response.json();
      displayPlayersForMerge(responseData);
    } catch (error) {
      let userHTML = `<div class = "playerAdminHeader">
    <h1>Current Unmatched Players</h1>
    </div>
      <div class = "playerAdmin">
      <h2>No Unmatched Players</h2>
      </div>`;
      adminPlayerContainer.innerHTML = userHTML;
    }
  }

  async function updateAdminStatus(userID, newStatus) {
    const response = await fetch(
      `http://localhost:5071/api/User/UpdateAdminStatus?userID=${userID}`,
      {
        method: "Patch",
        body: newStatus,
        headers: {
          "content-type": "application/json",
        },
      }
    );
    if (newStatus) {
      addClickEventToAdmins();
    } else {
      addClickEventToNonAdmins();
    }
  }

  function addClickEventToAdmins() {
    document.querySelectorAll(".userAdmin").forEach((user) => {
      user.addEventListener("click", function () {
        user.classList.replace("userAdmin", "userNonAdmin");
        let newHTML = user.innerHTML;
        newHTML = newHTML.replace("<p>Admin</p>", "<p>Not an Admin</p>");
        user.innerHTML = newHTML;
        updateAdminStatus(user.dataset.id, false);
      });
    });
  }

  function addClickEventToNonAdmins() {
    document.querySelectorAll(".userNonAdmin").forEach((user) => {
      user.addEventListener("click", function () {
        user.classList.replace("userNonAdmin", "userAdmin");
        let newHTML = user.innerHTML;
        newHTML = newHTML.replace("<p>Not an Admin</p>", "<p>Admin</p>");
        user.innerHTML = newHTML;
        updateAdminStatus(user.dataset.id, true);
      });
    });
  }

  async function mergePlayers(playerIDToKeep, playerIDToRemove) {
    const body = {
      keepPlayerID: playerIDToKeep,
      discardPlayerID: playerIDToRemove
    }
    const response = await fetch(`http://localhost:5071/api/User/MergePlayers`, {
      method: "Patch",
      body: JSON.stringify(body),
      headers: {
        "content-type": "application/json",
      },
    })
    if(response.status == 200)
      {
        alert("Player merged to existing user.");
        location.reload();
      }
      else
      {
        alert("Merge failed, please contact... no one. It just didn't work and you're out of luck...");
      }

  }

  function displayUsersForAdmin(allUsers) {
    let userHTML = `<div class = "userAdminHeader">
    <h1>User Admin Status</h1>
    <p>Click a user to toggle their admin status</p>
    </div>`;
    allUsers.forEach((user) => {
      if (user.userID != JSON.parse(localStorage.getItem("user")).UserID) {
        let userElement = "";
        if (user.isAdmin) {
          userElement = `
      <div class = "userAdmin" data-id="${user.userID}">
      <h2>${user.gamerTag}</h2>
      <p>${user.firstName} ${user.lastName}</p>
      <p>Admin</p>
      </div>`;
        } else {
          userElement = `
      <div class = "userNonAdmin" data-id="${user.userID}">
      <h2>${user.gamerTag}</h2>
      <p>${user.firstName} ${user.lastName}</p>
      <p>Not an Admin</p>
      </div>`;
        }
        userHTML += userElement;
      }
      adminUserContainer.innerHTML = userHTML;
      addClickEventToAdmins();
      addClickEventToNonAdmins();
    });
  }

  function clearSelectedUserForAdmin() {
    document.querySelectorAll(".userAdmin").forEach((user) => {
      user.classList.replace("userAdmin", "userNonAdmin");
    });
  }

  function clearSelectedPlayerForAdmin() {
    document.querySelectorAll(".selectedPlayerAdmin").forEach((player) => {
      player.classList.replace("selectedPlayerAdmin", "playerAdmin");
    });
  }

  function addClickEventToUsersForMerge() {
    document.querySelectorAll(".userNonAdmin").forEach((user) => {
      user.addEventListener("click", function () {
        clearSelectedUserForAdmin();
        user.classList.replace("userNonAdmin", "userAdmin");
        addClickEventToUsersForMerge();
        checkIfDisplayMergeButton();
      });
    });
  }

  function addClickEventToPlayersForMerge() {
    document.querySelectorAll(".playerAdmin").forEach((player) => {
      player.addEventListener("click", function () {
        clearSelectedPlayerForAdmin();
        player.classList.replace("playerAdmin", "selectedPlayerAdmin");
        addClickEventToPlayersForMerge();
        checkIfDisplayMergeButton();
      });
    });
  }

  function checkIfDisplayMergeButton() {
    if (
      document.querySelectorAll(".userAdmin").length > 0 &&
      document.querySelectorAll(".selectedPlayerAdmin").length > 0
    ) {
      mergePlayerButtonBox.classList.remove("hidden");
      mergeSelectedPlayerButton.addEventListener("click", function () {
        const usersPlayerID = document.querySelector(".userAdmin").dataset.id;
        const playersPlayerID = document.querySelector(".selectedPlayerAdmin")
          .dataset.id;
        mergePlayers(usersPlayerID, playersPlayerID);
      });
    } else {
      mergePlayerButtonBox.classList.add("hidden");
    }
  }

  function displayPlayersForMerge(unmatchedPlayers) {
    let userHTML = `<div class = "playerAdminHeader">
    <h1>Current Unmatched Players</h1>
    <p>Select one to merge</p>
    </div>`;
    unmatchedPlayers.forEach((user) => {
      let userElement = "";
      if (user.hasGamesPlayed) {
        userElement = `
      <div class = "playerAdmin" data-id="${user.playerID}">
      <h2>${user.playerName}</h2>
      <p>Has played games</p>
      </div>`;
      } else {
        userElement = `
      <div class = "playerAdmin" data-id="${user.playerID}">
      <h2>${user.playerName}</h2>
      <p>No played games</p>
      </div>`;
      }
      userHTML += userElement;
    });
    adminPlayerContainer.innerHTML = userHTML;
    addClickEventToPlayersForMerge();
  }

  function displayUsersForMerge(allUsers) {
    let userHTML = `<div class = "playerAdminHeader">
    <h1>Current Users</h1>
    <p>Select one to merge into</p>
    </div>`;
    allUsers.forEach((user) => {
      let userElement = "";
      userElement = `
      <div class = "userNonAdmin" data-id="${user.playerID}">
      <h2>${user.gamerTag}</h2>
      <p>${user.firstName} ${user.lastName}</p>
      </div>`;
      userHTML += userElement;
    });
    adminUserContainer.innerHTML = userHTML;
    addClickEventToUsersForMerge();
  }

  function UpdateSideBarForAdmin() {
    addGameButtonBox.classList.add("hidden");
    updateGameButtonBox.classList.add("hidden");
    removeGameButtonBox.classList.add("hidden");
    clearSelectionButtonBox.classList.add("hidden");
    viewPlayHistoryButtonBox.classList.add("hidden");
    recordGamePlayButtonBox.classList.add("hidden");
    returnToMainButtonBox.classList.remove("hidden");
    manageAdminButton.classList.add("hidden");
    mergePlayerButton.classList.add("hidden");
    returnToMainButton.addEventListener("click", function () {
      UpdateSideBarForAdminReturn();
    });
  }
  function UpdateSideBarForAdminReturn() {
    addGameButtonBox.classList.remove("hidden");
    viewPlayHistoryButtonBox.classList.remove("hidden");
    returnToMainButtonBox.classList.add("hidden");
    manageAdminButton.classList.remove("hidden");
    mergePlayerButton.classList.remove("hidden");
    mergePlayerButtonBox.classList.add("hidden");
    resetGameSelection();
    location.reload();
  }
  mergePlayerButton.addEventListener("click", function () {
    gamesContainer.style.display = "none";
    adminUserContainer.style.display = "flex";
    adminPlayerContainer.style.display = "flex";
    UpdateSideBarForAdmin();
    getUsersForAdmin(false);
    getPlayersForAdmin();
  });

  manageAdminButton.addEventListener("click", function () {
    gamesContainer.style.display = "none";
    adminUserContainer.style.display = "flex";
    UpdateSideBarForAdmin();
    getUsersForAdmin(true);
  });
});
