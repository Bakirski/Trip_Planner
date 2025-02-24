var bearerToken = localStorage.getItem("bearerToken");

window.onload = function () {
  console.log(window.location.pathname);
  getTrips();
};

async function getTrips() {
  try {
    const response = await fetch("http://localhost:5063/api/trips", {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
    });

    const trips = await response.json();
    console.log(trips);
    displayData(trips);
  } catch (error) {
    console.error("Error fetching trips: ", error);
  }
}

function escapeJSString(str) {
  return String(str).replace(/'/g, "\\'").replace(/\n/g, "\\n");
}

function displayData(data) {
  const dataContainer = document.getElementById("dataContainer");

  data.forEach((item) => {
    const dataItem = document.createElement("div");
    dataItem.classList.add("data-item");

    const destinationNames = item.destinations
      .map((dest) => dest.destinationName)
      .join(" | ");

    dataItem.innerHTML = `
      ${item.tripName}
      <button
       onclick="toggleUpdateForm(${item.id})">Update</button>
      <button onclick="deleteTrip(${item.id})">Delete</button>
      <div class="dropdown">
      <button class="dropbtn">Details</button>
      <div class="dropdown-content">
        <button 
        onclick="displayDetails(
        'Destinations: ${destinationNames}', 
        'Description: ${escapeJSString(item.description)}', 
        'Start Date: ${item.startDate}', 
        'End Date: ${item.endDate}')">
        Trip Details
        </button>
        <button onclick="destinationsRedirect(${item.id})">Destinations</button>
        <button onclick="activitiesRedirect(${item.id})">Activities</button>
        <button onclick="expensesRedirect(${item.id})">Expenses</button>
      `;
    dataContainer.appendChild(dataItem);
  });
}

function destinationsRedirect(id) {
  window.location.href = "/Pages/destinations.html";
  sessionStorage.setItem("tripId", id);
  console.log(sessionStorage.getItem("tripId"));
}

function activitiesRedirect(id) {
  window.location.href = "/Pages/activities.html";
  sessionStorage.setItem("tripId", id);
}

function expensesRedirect(id) {
  window.location.href = "/Pages/expenses.html";
  sessionStorage.setItem("tripId", id);
}

function displayDetails(destination, description, startDate, endDate) {
  const tripDetails = document.getElementById("tripDetails");
  tripDetails.classList.toggle("hidden");
  tripDetails.innerHTML = "";
  const details = [destination, description, startDate, endDate];

  details.forEach((detail) => {
    const item = document.createElement("p");
    item.innerHTML = detail;
    tripDetails.appendChild(item);
  });
}

function toggleCreateForm() {
  const displayForm = document.getElementById("createTripForm");
  displayForm.classList.toggle("hidden");
}

document
  .getElementById("createTripForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const tripName = document.getElementById("tripName").value;
    const tripDestination = document.getElementById("destination").value;
    const tripDescription = document.getElementById("description").value;
    const startDate = document.getElementById("startDate").value;
    const endDate = document.getElementById("endDate").value;

    await createTrip(
      tripName,
      tripDestination,
      tripDescription,
      startDate,
      endDate
    );
    location.reload();
  });

async function createTrip(
  tripName,
  tripDestination,
  tripDescription,
  startDate,
  endDate
) {
  try {
    const response = await fetch("http://localhost:5063/api/trips", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
      body: JSON.stringify({
        trip: {
          TripName: tripName,
          Description: tripDescription,
          StartDate: startDate,
          EndDate: endDate,
        },
        destination: {
          DestinationName: tripDestination,
        },
      }),
    });
    const data = await response.json();
    console.log(data);
  } catch (error) {
    console.error("Error creating new trip: ", error);
  }
}

function toggleUpdateForm(id) {
  sessionStorage.setItem("id", id);
  const displayForm = document.getElementById("updateTripForm");
  displayForm.classList.toggle("hidden");
}

document
  .getElementById("updateTripForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const id = sessionStorage.getItem("id");
    const tripName = document.getElementById("updateTripName").value;
    const tripDescription = document.getElementById("updateDescription").value;
    const startDate = document.getElementById("updateStartDate").value;
    const endDate = document.getElementById("updateEndDate").value;

    await updateTrip(id, tripName, tripDescription, startDate, endDate);
    location.reload();
  });

async function updateTrip(id, tripName, tripDescription, startDate, endDate) {
  try {
    const response = await fetch(`http://localhost:5063/api/trips/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
      body: JSON.stringify({
        TripName: tripName,
        Description: tripDescription,
        StartDate: startDate,
        EndDate: endDate,
      }),
    });
    alert("Trip updated successfully.");
    updateDestination(id, tripDestination);
    sessionStorage.removeItem("id");
  } catch (error) {
    console.error("Error updating trip: ", error);
  }
}

async function updateDestination(tripId, destination) {
  try {
    const response = await fetch(
      `http://localhost:5063/api/trips/${tripId}/destinations`,
      {
        method: "PATCH",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
        body: JSON.stringify({
          DestinationName: destination,
        }),
      }
    );
  } catch (error) {
    console.log("Error updating destination: ", error);
  }
}

async function deleteTrip(id) {
  try {
    const reponse = await fetch(`http://localhost:5063/api/trips/${id}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
    });
    alert("Trip deleted successfully.");
    location.reload();
  } catch (error) {
    console.error("Error deleting trip: ", error);
  }
}

//USER DETAIL FETCH
/*
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
*/
