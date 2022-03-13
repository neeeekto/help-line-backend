import React, { useEffect, useMemo } from "react";
import { useMigrationsQuery } from "@entities/migrations/queries";
import css from "./migrations.module.scss";
import { Card, Spin } from "antd";
import { MigrationsView } from "@views/migrations/migrations.view";

export const MigrationsProvider: React.FC = ({ children }) => {
  const migrationsQuery = useMigrationsQuery();

  useEffect(() => {
    const hasMigrations = migrationsQuery.data?.some((x) => !x.applied);
    let interval: any = 0;
    if (hasMigrations) {
      interval = setInterval(() => {
        migrationsQuery.refetch();
      }, 10000);
    }
    return () => clearInterval(interval);
  }, [migrationsQuery]);

  return (
    <div className={css.wrapper}>
      {children}
      {migrationsQuery.isLoading && !migrationsQuery.isFetched && (
        <div className={css.loader}>
          <Card size="small">
            <Spin tip="Check migrations..." />
          </Card>
        </div>
      )}
      {migrationsQuery.data?.some((x) => !x.applied) && (
        <div className={css.loader}>
          <MigrationsView migrations={migrationsQuery.data!} />
        </div>
      )}
    </div>
  );
};
