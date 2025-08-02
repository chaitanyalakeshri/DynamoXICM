// Auto-generated using Ruby

namespace WSClass.WSStructure
{
  public class NaPollutants
  {
    public long RubyId { get; set; }
    public string Determinant { get; set; }
    public double RainfallConc { get; set; }
    public double GroundwaterConc { get; set; }
    public double RdiiConc { get; set; }
    public bool SnowBuildUp { get; set; }
  }

  public class PotFactors
  {
    public long RubyId { get; set; }
    public string Determinant { get; set; }
    public string SedimentFraction { get; set; }
    public double PotencyFactor { get; set; }
  }

  public class StorageArray
  {
    public long RubyId { get; set; }
    public double Level { get; set; }
    public double Area { get; set; }
    public double Perimeter { get; set; }
  }

  public class Hyperlinks
  {
    public long RubyId { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
  }

  public class Geometry
  {
    public long RubyId { get; set; }
    public double Height { get; set; }
    public double Left { get; set; }
    public double Right { get; set; }
  }

  public class HdpTable
  {
    public long RubyId { get; set; }
    public double Head { get; set; }
    public double Discharge { get; set; }
    public double Power { get; set; }
  }

  public class LateralLinks
  {
    public long RubyId { get; set; }
    public string NodeId { get; set; }
    public string LinkSuffix { get; set; }
    public float Weight { get; set; }
  }

  public class RefhDescriptors
  {
    public long RubyId { get; set; }
    public double Bfihost { get; set; }
    public double Propwet { get; set; }
    public double Dplbar { get; set; }
    public double Dpsbar { get; set; }
    public double Urbext1990 { get; set; }
    public double Urbext2000 { get; set; }
    public long UrbextChoice { get; set; }
    public long CmaxMethod { get; set; }
    public double CmaxFactor { get; set; }
    public long TpMethod { get; set; }
    public double TpFactor { get; set; }
    public long UpMethod { get; set; }
    public double UpFactor { get; set; }
    public long UkMethod { get; set; }
    public double UkFactor { get; set; }
    public long BlMethod { get; set; }
    public double BlFactor { get; set; }
    public long BrMethod { get; set; }
    public double BrFactor { get; set; }
    public string ModelType { get; set; }
    public bool Isdirty { get; set; }
    public string Country { get; set; }
    public string Scale { get; set; }
    public double Saar { get; set; }
    public string Refh2Version { get; set; }
    public double CmaxValue { get; set; }
    public string CmaxFlag { get; set; }
    public double TpValue { get; set; }
    public string TpFlag { get; set; }
    public double UkValue { get; set; }
    public string UkFlag { get; set; }
    public double UpValue { get; set; }
    public string UpFlag { get; set; }
    public double BlValue { get; set; }
    public string BlFlag { get; set; }
    public double BrValue { get; set; }
    public string BrFlag { get; set; }
  }

  public class SudsControls
  {
    public long RubyId { get; set; }
    public string Id { get; set; }
    public string SudsStructure { get; set; }
    public string ControlType { get; set; }
    public double Area { get; set; }
    public long NumUnits { get; set; }
    public double AreaSubcatchmentPct { get; set; }
    public double UnitSurfaceWidth { get; set; }
    public double InitialSaturationPct { get; set; }
    public double ImperviousAreaTreatedPct { get; set; }
    public string OutflowTo { get; set; }
    public string DrainToSubcatchment { get; set; }
    public string DrainToNode { get; set; }
    public string Surface { get; set; }
    public double PerviousAreaTreatedPct { get; set; }
  }

  public class SwmmCoverage
  {
    public long RubyId { get; set; }
    public string LandUse { get; set; }
    public double Area { get; set; }
  }

  public class Profile
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Z { get; set; }
    public double RoughnessCw { get; set; }
    public double RoughnessManning { get; set; }
    public bool NewPanel { get; set; }
    public double RoughnessN { get; set; }
  }

  public class ChainageElevation
  {
    public long RubyId { get; set; }
    public double Chainage { get; set; }
    public double Elevation { get; set; }
  }

  public class SectionSpacing
  {
    public long RubyId { get; set; }
    public string Key { get; set; }
    public double Spacing { get; set; }
  }

  public class LeftBank
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double DischargeCoeff { get; set; }
    public double ModularRatio { get; set; }
    public string SectionMarker { get; set; }
    public string RtcDefinition { get; set; }
  }

  public class RightBank
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double DischargeCoeff { get; set; }
    public double ModularRatio { get; set; }
    public string SectionMarker { get; set; }
    public string RtcDefinition { get; set; }
  }

  public class Sections
  {
    public long RubyId { get; set; }
    public string Key { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double RoughnessN { get; set; }
    public bool NewPanel { get; set; }
  }

  public class Conveyance
  {
    public long RubyId { get; set; }
    public string Key { get; set; }
    public double Depth { get; set; }
    public double Conveyancevalue { get; set; }
    public double Area { get; set; }
    public double Width { get; set; }
    public double Perimeter { get; set; }
  }

  public class SectionArray
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double RoughnessN { get; set; }
    public bool NewPanel { get; set; }
  }

  public class FeTable
  {
    public long RubyId { get; set; }
    public double Flow { get; set; }
    public double Efficiency { get; set; }
  }

  public class BankArray
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double DischargeCoeff { get; set; }
    public double ModularRatio { get; set; }
    public string RtcDefinition { get; set; }
  }

  public class BridgeDeck
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double RoughnessN { get; set; }
    public bool NewPanel { get; set; }
    public string OpeningId { get; set; }
    public string OpeningSide { get; set; }
  }

  public class DsBridgeSection
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double RoughnessN { get; set; }
    public bool NewPanel { get; set; }
    public string OpeningId { get; set; }
    public string OpeningSide { get; set; }
  }

  public class DsLinkSection
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double RoughnessN { get; set; }
    public bool NewPanel { get; set; }
    public string OpeningId { get; set; }
    public string OpeningSide { get; set; }
  }

  public class UsBridgeSection
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double RoughnessN { get; set; }
    public bool NewPanel { get; set; }
    public string OpeningId { get; set; }
    public string OpeningSide { get; set; }
  }

  public class UsLinkSection
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double RoughnessN { get; set; }
    public bool NewPanel { get; set; }
    public string OpeningId { get; set; }
    public string OpeningSide { get; set; }
  }

  public class Piers
  {
    public long RubyId { get; set; }
    public string Id { get; set; }
    public double Offset { get; set; }
    public double Elevation { get; set; }
    public double Width { get; set; }
    public double RoughnessN { get; set; }
  }

  public class Bank
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double DischargeCoeff { get; set; }
    public double ModularRatio { get; set; }
    public string RtcDefinition { get; set; }
  }

  public class OffSections
  {
    public long RubyId { get; set; }
    public double CrossChainage { get; set; }
    public double Z { get; set; }
    public double Opening { get; set; }
    public double DeckLevel { get; set; }
  }

  public class HudpTable
  {
    public long RubyId { get; set; }
    public double Head { get; set; }
    public double UnitDischarge { get; set; }
  }

  public class LevelSections
  {
    public long RubyId { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public string VertexElevType { get; set; }
    public double Elevation { get; set; }
    public double ElevAdjust { get; set; }
  }

  public class BuildUp
  {
    public long RubyId { get; set; }
    public string Determinant { get; set; }
    public string BuildUpType { get; set; }
    public double MaxBuildUp { get; set; }
    public double PowerRateConstant { get; set; }
    public double PowerTimeExponent { get; set; }
    public double ExpRateConstant { get; set; }
    public double SaturationConstant { get; set; }
  }

  public class Washoff
  {
    public long RubyId { get; set; }
    public string Determinant { get; set; }
    public string WashoffType { get; set; }
    public double ExponentialWashoffCoeff { get; set; }
    public double RatingWashoffCoeff { get; set; }
    public double EmcWashoffCoeff { get; set; }
    public double WashoffExponent { get; set; }
    public double SweepRemoval { get; set; }
    public double BmpRemoval { get; set; }
  }

}
