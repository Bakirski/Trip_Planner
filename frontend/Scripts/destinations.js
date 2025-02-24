const tripId = sessionStorage.getItem("tripId");
const bearerToken = localStorage.getItem("bearerToken");

window.onload = function () {
  getDestinations(tripId);
};

async function getDestinations(id) {
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

    const destinationsResponse = await fetch(
      `http://localhost:5063/api/trips/${id}/destinations`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
      }
    );

    if (!destinationsResponse.ok) {
      const errorText = await destinationsResponse.text();
      throw new Error(
        errorText ||
          `Activities request failed with status ${tripResponse.status}`
      );
    }

    const tripData = await tripResponse.json();

    const destinationsData = await destinationsResponse.json();
    console.log(tripData);
    console.log(destinationsData);

    var data =
      destinationsData && destinationsData.length > 0
        ? [tripData, destinationsData]
        : [tripData];
    displayData(data);
  } catch (error) {
    console.log("Error fetching data: ", error);
    alert(error.message);
  }
}

function displayData(data) {
  const destinationsContainer = document.getElementById(
    "destinationsContainer"
  );

  const tripTitle = document.getElementById("tripTitle");
  tripTitle.innerHTML = `Activities for "${data[0].tripName}"`;

  if (data.length > 1) {
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
