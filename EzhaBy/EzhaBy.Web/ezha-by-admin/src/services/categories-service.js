import HttpRequest from './http-request';
import Emitter from './event-emitter';

export const CATEGORIES_LIST_UPDATED = 'TAG_LIST_UPDATED';

export default class CategoriesService {
  static getCategories(id) {
    return HttpRequest.Get(`api/catering-facilities/${id}/categories`);
  }

  static createCategory(id, name) {
    let data = {
      categoryName: name
    };
    return HttpRequest.Put(
      `api/catering-facilities/${id}/categories`,
      JSON.stringify(data)
    ).then(_ => Emitter.emit(CATEGORIES_LIST_UPDATED, {}));
  }
}
