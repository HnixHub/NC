New Combin challenge

Multitracks.com repo.

The following steps were done to complete this challenge:
1. Familiarize yourself with the code in the solution.

2. Familiarize yourself with the data structures in the SQL project.

3. Implement (Sync) Artist Details Page.

4. Find markup in the folder multitracks.com-dotNet\Web\multitracks.com\multitracks.com\PageToSync

5. Create a new page artistDetails.aspx using the provided markup'

6. Make the page data driven to display the appropriate images / text for a given parameter of artistID

7. The page should pull all the needed data from a Stored Procedure (GetArtistDetails)

8. The page should make use of the MTDataAccess Class Library. Look at the source of this page for an example. (ExecuteStoredProcedureDS will return a DataTable rather than a DataSet if multiple result sets are needed)

9. Implement an API by adding a project under the Web folder in the solution. Use the technology of your choice here (MS Stack). The API should accomplish these tasks:

  9.1. Endpoint to search for an artist by name (api.multitracks.com/artist/search)

  9.2. Endpoint to list all songs with paging support using page size, page number, etc. (api.multitracks.com/song/list)

  9.3. Endpoint to add an Artist to the Artist table (api.multitracks.com/artist/add)
