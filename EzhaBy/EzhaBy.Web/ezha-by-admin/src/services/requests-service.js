import HttpRequest from "./http-request";
import Emitter from "./event-emitter";

export const PARTNER_LIST_UPDATED = "PARTNER_LIST_UPDATED";

export default class RequestsService {
  static getPartnerRequests() {
    return HttpRequest.Get("api/requests/partners");
  }

  static changePartnerStatus(id, status) {
    const data = {
      requestStatus: status
    };
    return HttpRequest.Put(`api/requests/partners/${id}/status`, data).then(_ =>
      Emitter.emit(PARTNER_LIST_UPDATED, {})
    );
  }

  static sendEmail(email, subject, body) {
    const data = {
      email: email,
      subject: subject,
      body: body
    };
    return HttpRequest.Put("api/requests/email", data);
  }
}
