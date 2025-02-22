const tripId = sessionStorage.getItem("tripId");
const bearerToken = localStorage.getItem("bearerToken");

window.onload = function () {
  getTrip(tripId);
};

async function getTrip(id) {
  try {
    const response = await fetch(`http://localhost:5063/api/trips/${id}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
    });
    const response2 = await fetch(
      `http://localhost:5063/api/trips/${id}/destinations`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
      }
    );

    const tripData = await response.json();
    sessionStorage.setItem("tripId", tripData.id);
    const destinationData = await response2.json();

    console.log(tripData);
    console.log(destinationData);
    const data = [tripData, destinationData.length ? destinationData : []];
    displayData(data);
  } catch (error) {
    console.log("Error fetching data: ", error);
  }
}

function displayData(data) {
  const destinationsContainer = document.getElementById(
    "destinationsContainer"
  );

  const tripTitle = document.getElementById("tripTitle");
  tripTitle.textContent = data[0].tripName;

  if (data[1].length > 0) {
    data[1].forEach((item) => {
      const dataItem = document.createElement("p");
      dataItem.innerHTML = `
      ${item.destinationName}
      <button onclick="deleteDestination(${item.tripId}, ${item.id})"">Delete</button>`;
      destinationsContainer.appendChild(dataItem);
    });
  } else null;
}

async function deleteDestination(tripId, destinationId) {
  try {
    const response = await fetch(
      `http://localhost:5063/api/trips/${tripId}/destinations/${destinationId}`,
      {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
      }
    );

    alert("Destination successfully deleted.");
    location.reload();
  } catch (error) {
    console.error("Error deleting destination: ", error);
  }
}

function toggleDestinationForm() {
  const displayForm = document.getElementById("destinationForm");
  displayForm.classList.toggle("hidden");
}

document
  .getElementById("destinationForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const destinationName = document.getElementById("destinationName").value;
    createDestination(destinationName);
  });

async function createDestination(destinationName) {
  const id = sessionStorage.getItem("tripId");
  try {
    const response = await fetch(
      `http://localhost:5063/api/trips/${id}/destinations`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
        body: JSON.stringify({
          DestinationName: destinationName,
        }),
      }
    );

    const data = await response.json();
    console.log(data);
    alert("Destination created successfully.");
    location.reload();
  } catch (error) {
    console.error("Error creating destination: ", error);
  }
}
