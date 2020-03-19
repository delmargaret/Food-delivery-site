import axios from 'axios';
import ConfigService from './config-service';

export default class HttpRequest {
  static Get(endpoint) {
    const url = ConfigService.addBaseAddress(endpoint);
    const options = {
      url: url,
      method: 'GET',
      type: 'json',
      headers: {
        'Content-Type': 'application/json'
      }
    };
    return axios(options);
  }

  static Post(endpoint, data) {
    const url = ConfigService.addBaseAddress(endpoint);
    const options = {
      url: url,
      method: 'POST',
      data: data,
      headers: {
        'Content-Type': 'application/json'
      }
    };
    return axios(options).catch(error => {
      if (error.response) {
        console.log(error.response.data);
        return null;
      }
    });
  }

  static Delete(endpoint) {
    const url = ConfigService.addBaseAddress(endpoint);
    const options = {
      url: url,
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json'
      }
    };

    return axios(options).catch(error => {
      if (error.response) {
        console.log(error.response.data);
        return null;
      }
    });
  }

  static Put(endpoint, data) {
    const url = ConfigService.addBaseAddress(endpoint);
    const options = {
      url: url,
      method: 'PUT',
      data: data,
      headers: {
        'Content-Type': 'application/json'
      }
    };

    return axios(options).catch(error => {
      if (error.response) {
        console.log(error.response.data);
        return null;
      }
    });
  }
}
