import { createQueryKeys } from '@help-line/modules/query';
import {
  Project,
  User,
  RolesClientApi,
  RoleData,
  Role,
} from '@help-line/entities/client/api';
import { useApi } from '@help-line/modules/api';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { ROOT_QUERY_KEY } from '../constants';

export const clientRolesQueryKeys = createQueryKeys(
  [ROOT_QUERY_KEY, 'roles'],
  ({ makeKey }) => ({
    list: () => makeKey('list'),
  })
);

export const useRolesQuery = () => {
  const api = useApi(RolesClientApi);
  return useQuery(clientRolesQueryKeys.list(), () => api.get());
};

export const useCreateRoleMutation = () => {
  const api = useApi(RolesClientApi);
  const client = useQueryClient();

  return useMutation(
    [...clientRolesQueryKeys.root, 'create'],
    (data: RoleData) => api.add({ data }),
    {
      onSuccess: () => client.invalidateQueries(clientRolesQueryKeys.list()),
    }
  );
};

export const useUpdateRoleMutation = (roleId: Role['id']) => {
  const api = useApi(RolesClientApi);
  const client = useQueryClient();

  return useMutation(
    [...clientRolesQueryKeys.root, 'update', roleId],
    (data: RoleData) => api.update({ roleId, data }),
    {
      onSuccess: () => client.invalidateQueries(clientRolesQueryKeys.list()),
    }
  );
};

export const useDeleteRoleMutation = (roleId: Role['id']) => {
  const api = useApi(RolesClientApi);
  const client = useQueryClient();

  return useMutation(
    [...clientRolesQueryKeys.root, 'delete', roleId],
    (data: RoleData) => api.delete({ roleId }),
    {
      onSuccess: () => client.invalidateQueries(clientRolesQueryKeys.list()),
    }
  );
};
