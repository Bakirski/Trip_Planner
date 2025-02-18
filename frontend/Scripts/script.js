var bearerToken = "";

async function getTrip(tripId) {
  try {
    const response = await fetch(`http://localhost:5063/api/trips/${tripId}`, {
      headers: {
        Authorization: `Bearer ${bearerToken}`,
      },
    });

    const json = await response.json();
    console.log(json);
  } catch (error) {
    console.log("Error fetching data: ", error);
  }
}

//USER LOGIN
document
  .getElementById("loginForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const userEmail = document.getElementById("userEmail").value;
    const userPassword = document.getElementById("userPassword").value;
    try {
      await authenticateUser(userEmail, userPassword);
      window.location.href = "/Pages/trips.html";
    } catch (error) {
      console.error("Error authenticating user: ", error);
    }
  });

async function authenticateUser(userEmail, userPassword) {
  try {
    const response = await fetch("http://localhost:5063/api/auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        UserEmail: userEmail,
        Password: userPassword,
      }),
    });

    if (!response.ok) {
      const errorMessage = await response.text();
      throw new Error(errorMessage);
    }

    const data = await response.json();
    alert("Login Success!");
    localStorage.setItem("bearerToken", data.token);
    bearerToken = localStorage.getItem("bearerToken");
    console.log("Token: ", bearerToken);
    console.log(
      "Token saved in storage: ",
      localStorage.getItem("bearerToken")
    );
  } catch (error) {
    throw error;
  }
}

//USER REGISTRATION
document.addEventListener("DOMContentLoaded", () => {
  document
    .getElementById("registerForm")
    .addEventListener("submit", async (event) => {
      event.preventDefault();

      const username = document.getElementById("userName").value;
      const email = document.getElementById("userEmail").value;
      const password = document.getElementById("userPassword").value;
      const confirmPassword = document.getElementById("confirmPassword").value;

      try {
        await registerUser(username, email, password, confirmPassword);
        window.location.href = "/index.html";
      } catch (error) {
        console.error("Error registering user: ", error);
      }
    });

  async function registerUser(
    username,
    userEmail,
    userPassword,
    confirmPassword
  ) {
    try {
      const response = await fetch("http://localhost:5063/api/auth/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          Username: username,
          Email: userEmail,
          Password: userPassword,
          ConfirmPassword: confirmPassword,
        }),
      });

      if (!response.ok) {
        const errorMessage = await response.text();
        throw new Error(errorMessage);
      }

      const data = await response.json();
      console.log("Register Success: ", data);
    } catch (error) {
      throw error;
    }
  }
});

//USER DETAIL FETCH
async function getUser() {
  try {
    const response = await fetch("http://localhost:5063/api/user", {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
    });

    const userData = await response.json();
    console.log(userData.value);
  } catch (error) {
    console.error("Error getting user data: ", error);
  }
}
