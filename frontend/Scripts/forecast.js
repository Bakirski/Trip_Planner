document
  .getElementById("forecastForm")
  .addEventListener("submit", async (event) => {
    event.preventDefault();

    const locationName = document.getElementById("locationName").value;
    getForecast(locationName);
  });

async function getForecast(locationName) {
  const days = 3;
  const apiKey = "86dc608358604267a1691655251202";
  try {
    const response = await fetch(
      `https://api.weatherapi.com/v1/forecast.json?key=${apiKey}&q=${locationName}&days=${days}&aqi=no&alerts=no`,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    const data = await response.json();
    console.log(data);
    displayData(data);
  } catch (error) {
    console.log(error);
  }
}

function displayData(data) {
  const forecastContainer = document.getElementById("forecastContainer");
  forecastContainer.innerHTML = "";
  const locationData = document.createElement("div");
  locationData.classList.add("location-details");

  locationData.innerHTML = `
      <p>Location Name: ${data.location.name}</p>
      <p>Region: ${data.location.region}</p>
      <p>Country: ${data.location.country}</p>`;
  forecastContainer.appendChild(locationData);

  const container = document.createElement("div");
  container.classList.add("weather-card-container");
  forecastContainer.appendChild(container);

  data.forecast.forecastday.forEach((day) => {
    const dataItem = document.createElement("div");
    dataItem.classList.add("weather-card");
    dataItem.innerHTML = `
      <p>Date: ${day.date}</p>
      <img src="${day.day.condition.icon}" alt="${day.day.condition.text}"/>
      <p>${day.day.condition.text}</p>
      <p>Average: ${day.day.avgtemp_c}°C</p>
      <p>Minimum: ${day.day.mintemp_c}°C</p>
      <p>Maximum: ${day.day.maxtemp_c}°C</p>`;

    const button = document.createElement("button");
    button.textContent = "Hourly Forecast";
    button.addEventListener("click", () => displayHourlyForecast(day.hour));
    dataItem.appendChild(button);
    container.appendChild(dataItem);
  });
}

function displayHourlyForecast(hours) {
  const hourlyContainer = document.getElementById("hourlyForecastContainer");
  hourlyContainer.innerHTML = "";

  hours.forEach((hour) => {
    const dataItem = document.createElement("div");
    dataItem.classList.add("hour-card");

    dataItem.innerHTML = `
        <p>${hour.time.split(" ")[1]}</p>
        <img src = "${hour.condition.icon}" alt = "${hour.condition.text}"/>
        <p>${hour.condition.text}</p>
        <p>Temperature: ${hour.temp_c}°C</p>
        <p>Feels like: ${hour.feelslike_c}°C</p>`;

    hourlyContainer.appendChild(dataItem);
  });
}
