import { AuctionSystemTemplatePage } from './app.po';

describe('AuctionSystem App', function() {
  let page: AuctionSystemTemplatePage;

  beforeEach(() => {
    page = new AuctionSystemTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
