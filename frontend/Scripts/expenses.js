const bearerToken = localStorage.getItem("bearerToken");
const tripId = sessionStorage.getItem("tripId");

window.onload = function () {
  getExpenses(tripId);
};

async function getExpenses(id) {
  try {
    const tripResponse = await fetch(`http://localhost:5063/api/trips/${id}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
    });

    if (!tripResponse.ok) {
      const errorText = await tripResponse.text();
      throw new Error(
        errorText || `Trip request failed with status ${tripResponse.status}`
      );
    }

    const expensesResponse = await fetch(
      `http://localhost:5063/api/trips/${id}/expenses`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
      }
    );

    if (!expensesResponse.ok) {
      const errorText = await expensesResponse.text();
      throw new Error(errorText);
    }

    const tripData = await tripResponse.json();
    const expensesData = await expensesResponse.json();
    console.log(tripData, expensesData);

    const data =
      expensesData && expensesData.length > 0
        ? [tripData, expensesData]
        : [tripData];

    displayData(data);
  } catch (error) {
    console.log("Error fetching data: ", error);
    alert(error.message);
  }
}

function displayData(data) {
  const expensesContainer = document.getElementById("expensesContainer");

  const tripTitle = document.getElementById("tripTitle");
  tripTitle.innerHTML = `Expenses for "${data[0].tripName}"`;

  if (data.length > 1) {
    data[1].forEach((item) => {
      const dataItem = document.createElement("p");
      dataItem.innerHTML = `
          ${item.expenseName}, $${item.expenseAmount}
          <button onclick="deleteExpense(${item.tripId}, ${item.id})"">Delete</button>`;
      expensesContainer.appendChild(dataItem);
    });
  } else null;
}

function toggleExpensesForm() {
  document.getElementById("expensesForm").classList.toggle("hidden");
}

document
  .getElementById("expensesForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const expenseName = document.getElementById("expenseName").value;
    const expenseAmount = document.getElementById("expenseAmount").value;

    createExpense(expenseName, expenseAmount);
  });

async function createExpense(expenseName, expenseAmount) {
  try {
    const response = await fetch(
      `http://localhost:5063/api/trips/${tripId}/expenses`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
        body: JSON.stringify({
          ExpenseName: expenseName,
          ExpenseAmount: expenseAmount,
        }),
      }
    );

    const data = await response.json();
    console.log(data);
    alert("New Expense successfully added.");
    location.reload();
  } catch (error) {
    console.error("Error creating activity: ", error);
  }
}

async function deleteExpense(tripId, expenseId) {
  try {
    const response = await fetch(
      `http://localhost:5063/api/trips/${tripId}/expenses/${expenseId}`,
      {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
      }
    );
    alert("Expense deleted successfully.");
    location.reload();
  } catch (error) {
    console.error("Error deleting expense: ", error);
  }
}
