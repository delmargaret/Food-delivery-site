import HttpRequest from './http-request';

export default class CateringFacilitiesService {
  static createCateringFacility(
    name,
    deliveryTime,
    deliveryPrice,
    type,
    workingHours,
    town,
    street,
    house,
    tagIds
  ) {
    const data = {
      cateringFacilityName: name,
      deliveryTime: deliveryTime,
      deliveryPrice: deliveryPrice,
      cateringFacilityType: type,
      workingHours: workingHours,
      town: town,
      street: street,
      houseNumber: house,
      CateringFacilityTagIds: tagIds
    };
    return HttpRequest.Post(`api/catering-facilities`, JSON.stringify(data));
  }
}
