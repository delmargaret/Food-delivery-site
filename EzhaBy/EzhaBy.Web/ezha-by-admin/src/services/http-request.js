import axios from "axios";
import ConfigService from "./config-service";

function HandleException(ex){
  console.log(`Exception: ${ex.Message}`);
}

export default class HttpRequest {
  static Get(endpoint) {
    const url = ConfigService.addBaseAddress(endpoint);
    const options = {
      url: url,
      method: "GET",
      type: "json",
      headers: {
        "Content-Type": "application/json"
      }
    };
    return axios(options).catch(error => {
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
      headers: {
        "Content-Type": "application/json"
      }
    };
    return axios(options).catch(error => {
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
      headers: {
        "Content-Type": "application/json"
      }
    };

    return axios(options).catch(error => {
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
      headers: {
        "Content-Type": "application/json"
      }
    };

    return axios(options).catch(error => {
      if (error.response) {
        HandleException(error.response.data);
      }
    });
  }
}
