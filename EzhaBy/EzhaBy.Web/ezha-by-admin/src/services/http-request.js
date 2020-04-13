import axios from "axios";
import ConfigService from "./config-service";

function HandleException(ex) {
  console.log(`Exception: ${ex.Message}`);
}

export default class HttpRequest {
  static makeAuthorizationHeaderValue(token) {
    return `Bearer ${token}`;
  }

  static makeHeaders() {
    const headers = {
      "Content-Type": "application/json",
    };

    const token = localStorage.getItem("token");

    if (token) {
      headers.Authorization = this.makeAuthorizationHeaderValue(token);
    }

    return headers;
  }

  static Get(endpoint) {
    const url = ConfigService.addBaseAddress(endpoint);
    const options = {
      url: url,
      method: "GET",
      type: "json",
      headers: this.makeHeaders(),
    };
    return axios(options).catch((error) => {
      if (error.response) {
        HandleException(error.response.data);
      }
    });
  }

  static Post(endpoint, data) {
    const url = ConfigService.addBaseAddress(endpoint);
    const options = {
      url: url,
      method: "POST",
      data: data,
      headers: this.makeHeaders(),
    };
    return axios(options).catch((error) => {
      if (error.response) {
        HandleException(error.response.data);
      }
    });
  }

  static Delete(endpoint) {
    const url = ConfigService.addBaseAddress(endpoint);
    const options = {
      url: url,
      method: "DELETE",
      headers: this.makeHeaders(),
    };

    return axios(options).catch((error) => {
      if (error.response) {
        HandleException(error.response.data);
      }
    });
  }

  static Put(endpoint, data) {
    const url = ConfigService.addBaseAddress(endpoint);
    const options = {
      url: url,
      method: "PUT",
      data: data,
      headers: this.makeHeaders(),
    };

    return axios(options).catch((error) => {
      if (error.response) {
        HandleException(error.response.data);
      }
    });
  }
}
