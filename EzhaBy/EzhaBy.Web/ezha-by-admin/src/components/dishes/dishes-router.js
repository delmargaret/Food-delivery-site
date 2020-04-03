import React from "react";
import { Switch, Route, useRouteMatch, withRouter } from "react-router-dom";

import DishesPage from "./dishes";
import AddDish from "./add-dish";
import UpdateDish from "./update-dish";

export default function() {
  let { path } = useRouteMatch();

  const AddDishWithRouter = withRouter(AddDish);
  const UpdateDishWithRouter = withRouter(UpdateDish);

  return (
    <Switch>
      <Route exact path={path}>
        <DishesPage />
      </Route>
      <Route path={`${path}/new/:cateringFacilityId`}>
        <AddDishWithRouter />
      </Route>
      <Route path={`${path}/edit/:id/:cateringFacilityId`}>
        <UpdateDishWithRouter />
      </Route>
    </Switch>
  );
}
