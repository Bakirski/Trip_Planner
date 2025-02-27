document
  .getElementById("registerForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();
    console.log("Form submission intercepted.");

    const username = document.getElementById("userName").value;
    const email = document.getElementById("userEmail").value;
    const password = document.getElementById("userPassword").value;
    const confirmPassword = document.getElementById("confirmPassword").value;

    try {
      await registerUser(username, email, password, confirmPassword);
      window.location.href = "/";
    } catch (error) {
      console.error("Error registering user: ", error);
      alert(error);
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
    alert("You have registered successfully.");
  } catch (error) {
    throw error;
  }
}
